using System.Net;
using ConnectFour.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ConnectFour.Middleware;

public class ExceptionProblemHandler
{
  private readonly RequestDelegate request;
  private readonly ILogger<ExceptionProblemHandler> logger;
  private readonly IHostEnvironment env;

  public ExceptionProblemHandler(
    RequestDelegate request,
    ILogger<ExceptionProblemHandler> logger,
    IHostEnvironment env)
  {
    this.request = request;
    this.logger = logger;
    this.env = env;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await request(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An unhandled error has occurred");
      await HandleExceptionAsync(context, ex);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var status = exception switch
    {
      ArgumentException => (int)HttpStatusCode.BadRequest,
      UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
      NotFoundException => (int)HttpStatusCode.NotFound,
      DbUpdateException => (int)HttpStatusCode.Conflict,
      _ => (int)HttpStatusCode.InternalServerError
      //discard pattern, default case for anything not covered above
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = status;

    var response = new
    {
      error = exception switch
      {
        ArgumentException => "Invalid input",
        UnauthorizedAccessException => "Access denied",
        NotFoundException => "Resource not found",
        DbUpdateException => "An error occurred in the database",
        _ => "An unexpected error has occurred"
      },
      details = env.IsDevelopment() ? exception.Message : null,
      type = exception.GetType().Name
    };

    return context.Response.WriteAsJsonAsync(response);
  }
}