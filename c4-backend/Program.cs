using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using ConnectFour.Middleware;
using ConnectFour.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<PlayerServices>();
builder.Services.AddScoped<GameServices>();
builder.Services.AddScoped<RoomServices>();
builder.Services.AddDbContext<AppDatabase>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDatabase>();

var secretKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Secret Key not found in configuration");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "connectapp",
        ValidAudience = "connectapp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// ========================================================================
app.UseWebSockets();

app.Map("w/s", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await Echo(context, webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});
// ========================================================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionProblemHandler>();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (args.Contains("--migrate"))
{
	await using var scope = app.Services.CreateAsyncScope();
	var db = scope.ServiceProvider.GetRequiredService<AppDatabase>();
	await db.Database.MigrateAsync();
}

app.Run();


// ========================================================================
// basic echo handler, this is where i'll add my own stuff for send room invite etc..
static async Task Echo(HttpContext context, WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

    while (!result.CloseStatus.HasValue)
    {
        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    }

    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}
// ========================================================================
