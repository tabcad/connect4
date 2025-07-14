using ConnectFour.Contracts;
using ConnectFour.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFour.Controllers;

[Route("api/game")]
[ApiController]

public class GameController : ControllerBase
{
  private readonly AppDatabase db;
  private readonly GameServices service;

  public GameController(AppDatabase db, GameServices service)
  {
    this.db = db;
    this.service = service;
  }

  /// <summary>
  /// Places a piece based on player's turn
  /// </summary>
  /// <param name="data"></param>
  /// <returns></returns>
  [HttpPost("move")]
  public async Task<IActionResult> GameTurn([FromBody] PlayersMoveContract data)
  {
    var response = await service.GameTurnAsync(data.PlayerId, data.GameId, data.Col);
    return Ok(response);
  }

  [HttpPost("bot/{gameId:Guid}")]
  public async Task<IActionResult> BotTurn([FromRoute] Guid gameId)
  {
    var response = await service.BotTurnAsync(gameId);
    return Ok(response);
  }

  /// <summary>
  /// Restarts a game using the id
  /// </summary>
  /// <param name="gameId"></param>
  /// <returns></returns>
  [HttpPost("restart/{gameId:Guid}")]
  [ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ResetGame([FromRoute] Guid gameId)
  {
    var newGame = await service.RestartCurrentGameAsync(gameId);

    return Ok(newGame);
  }
}