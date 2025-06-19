using ConnectFour.Contracts;
using ConnectFour.Services;
using Microsoft.AspNetCore.Authorization;
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
  /// Creates a new game for 2 players
  /// </summary>
  /// <param name="data"></param>
  /// <returns></returns>
  [HttpPost]
  public async Task<IActionResult> CreateNewGame([FromBody] NewGameRequest data)
  {
    var newGame = await service.CreateNewGameAsync(data.PlayerOne, data.PlayerTwo);

    return Ok(new GameResponse
    {
      GameId = newGame.GameId,
      CurrentTurn = newGame.CurrentTurn,
      Board = newGame.Board
    });
  }

  [HttpGet("{id:Guid}")]
  public async Task<IActionResult> GetGameById([FromRoute] Guid id)
  {
    var game = await service.GetGameByIdAsync(id);

    return Ok(new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurn,
      Board = game.Board
    });
  }

  /// <summary>
  /// Returns list of all games created
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [ProducesResponseType(typeof(List<AllGamesResponse>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetAllPlayers()
  {
    var list = await service.ListAllGamesAsync();

    return Ok(list);
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