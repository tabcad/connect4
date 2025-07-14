using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConnectFour.Contracts;
using ConnectFour.Exceptions;
using ConnectFour.Middleware;
using ConnectFour.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConnectFour.Services;

public class PlayerServices
{
  private readonly AppDatabase db;
  private readonly UserManager<IdentityUser> userManager;
  private readonly SignInManager<IdentityUser> signInManager;
  private readonly IConfiguration configuration;

  public PlayerServices(
    AppDatabase db,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IConfiguration configuration)
  {
    this.db = db;
    this.userManager = userManager;
    this.signInManager = signInManager;
    this.configuration = configuration;
  }

  internal async Task<Player> GetPlayerByIdAsync(Guid id)
  {
    if (id == Guid.Empty)
    {
      throw new ArgumentException("Id must be a valid GUID");
    }

    var player = await db.Players.FirstOrDefaultAsync(x => x.Id == id)
    ?? throw new NotFoundException($"No account found for id: {id}");

    return player;
  }

  internal async Task<List<AccountResponse>> GetAllPlayersAsync()
  {
    var users = await db.Players.ToListAsync();
    var response = users.Select(x => new AccountResponse
    {
      Id = x.Id,
      Username = x.Username,
      Wins = x.Wins,
      Losses = x.Losses
    }).ToList();

    return response;
  }

  internal async Task<(Player User, string Token)> CreateAccountAsync(string username, string password)
  {
    var newUser = new IdentityUser { UserName = username };

    var result = await userManager.CreateAsync(newUser, password);

    if (!result.Succeeded)
    {
      var errors = result.Errors.Select(e => e.Description).ToList();

      if (errors.Any(e => e.Contains("is already taken", StringComparison.OrdinalIgnoreCase)))
      {
        throw new ArgumentException("Username taken");
      }
      throw new ArgumentException($"Failed to create account: {string.Join(", ", errors)}");
    }

    var user = new Player
    {
      Id = Guid.NewGuid(),
      Username = username,
      Wins = 0,
      Losses = 0
    };

    db.Players.Add(user);
    await db.SaveChangesAsync();

    var token = GenerateJwtToken(newUser);

    return (user, token);
  }

  internal async Task<(Player User, string Token)> LoginAsync(string username, string password)
  {
    var login = await userManager.FindByNameAsync(username);

    if (login != null && await userManager.CheckPasswordAsync(login, password))
    {
      var token = GenerateJwtToken(login);

      var user = await db.Players.FirstOrDefaultAsync(x => x.Username == username);

      return (user!, token!);
    }
    throw new UnauthorizedAccessException("Invalid username or password.");
  }

  private string GenerateJwtToken(IdentityUser user)
  {

    var secretKey = configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(secretKey))
    {
      throw new InvalidOperationException("JWT secret key is not configured.");
    }

    var claims = new[]
    {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //add claim for id for admin permissions
        };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  internal async Task<List<RoomListResponse>> ListUsersRoomsAsync(Guid playerId)
  {
    var gameIds = await db.Games
      .Where(x => x.PlayerOneId == playerId || x.PlayerTwoId == playerId)
      .Select(x => x.GameId)
      .ToListAsync();

    var rooms = await db.Rooms
      .Where(x => gameIds.Contains(x.GameId))
      .Select(x => new RoomListResponse { Id = x.RoomId })
      .ToListAsync();

    return rooms;
  }
}
