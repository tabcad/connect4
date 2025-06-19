using ConnectFour.Contracts;
using ConnectFour.Services;
using Microsoft.AspNetCore.Authorization;
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

  /// <summary>
  /// Gets players details for a given id
  /// </summary>
  /// <param name="id">GUID of player to get</param>
  /// <returns></returns>
  [AllowAnonymous]
  [HttpGet("{id:Guid}")]
  [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesDefaultResponseType]
  public async Task<IActionResult> GetAccount([FromRoute] Guid id)
  {
    var user = await services.GetPlayerByIdAsync(id);

    return Ok(new AccountResponse
    {
      Id = user.Id,
      Username = user.Username,
      Wins = user.Wins,
      Losses = user.Losses
    });
  }

  /// <summary>
  /// Returns list of details for all players
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [ProducesResponseType(typeof(List<AccountResponse>), StatusCodes.Status200OK)]
  [ProducesDefaultResponseType]
  public async Task<IActionResult> GetAllPlayers()
  {
    var list = await services.GetAllPlayersAsync();

    return Ok(list);
  }

  /// <summary>
  /// Creates new account, returns a token
  /// </summary>
  /// <param name="data"></param>
  /// <returns></returns>
  [HttpPost("signup")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> CreateAccount([FromBody] CreateUserContract data)
  {
    if (string.IsNullOrWhiteSpace(data.Username) || data.Username.Length < 3 || data.Username.Length > 64)
    {
      throw new ArgumentException("Username must be longer than 3 characters and less than 64");
    }

    var (user, token) = await services.CreateAccountAsync(data.Username, data.Password);

    return Ok(new LoginResponse
    {
      Token = token,
      Id = user.Id,
      Username = user.Username,
    });
  }

  /// <summary>
  /// Returns a token for an existing account
  /// </summary>
  /// <param name="data"></param>
  /// <returns></returns>
  [HttpPost("login")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
  public async Task<IActionResult> Login([FromBody] LoginContract data)
  {
    var (user, token) = await services.LoginAsync(data.Username, data.Password);

    return Ok(new LoginResponse
    {
      Token = token,
      Username = user.Username,
      Id = user.Id
    });
  }
}