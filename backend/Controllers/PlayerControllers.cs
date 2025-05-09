using ConnectFour.Contracts;
using ConnectFour.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFour.Controllers;

[Route("api/user")]
[ApiController]
public class PlayerController : ControllerBase
{
  private readonly AppDatabase db;
  private readonly PlayerServices services;

  public PlayerController(
    PlayerServices services, AppDatabase db)
  {
    this.services = services;
    this.db = db;
  }

//need to throw 400 when password doesnt meet standards
  [HttpPost("signup")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateAccount([FromBody] CreateUserContract data)
  {
    var (user, token) = await services.CreateAccountAsync(data.Username, data.Password);

    return Ok(new LoginResponse
    {
      Token = token,
      Id = user.Id,
      Username = user.Username,
    });
  }

  [HttpPost("login")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
  public async Task<IActionResult> Login ([FromBody] LoginContract data)
  {
    var (user, token) = await services.LoginAsync(data.Username, data.Password);

    return Ok(new LoginResponse
    {
      Token = token,
      Username = user.Username,
      Id = user.Id
    });
  }

  [HttpGet]
  [ProducesResponseType(typeof(List<AccountResponse>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetAllUsers()
  {
    var list = await services.GetAllUsersAsync();

    return Ok(list);
  }
}