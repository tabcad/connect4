using ConnectFour.Contracts;
using ConnectFour.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFour.Controllers;

[Route("api/room")]
[ApiController]

public class RoomController : ControllerBase
{
  private readonly AppDatabase db;
  private readonly RoomServices service;
  public RoomController(AppDatabase db, RoomServices service)
  {
    this.db = db;
    this.service = service;
  }

  [HttpPost("room")]
  [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<RoomResponse> CreateNewRoom(Guid playerOne, Guid playerTwo)
  {

   var newRoom = await service.CreateTwoPlayerRoomAsync(playerOne, playerTwo);

    return Ok(new RoomResponse
    {
      RoomId = newRoom.RoomId,
      PlayerOne = newRoom.PlayerOne,
      PlayerTwo = newRoom.PlayerTwo,
      Game = newRoom.Game,
      GameId = newRoom.GameId,
      CreatedOnUtc = newRoom.CreatedOnUtc,
      IsActive = newRoom.IsActive,
      CurrentTurn = newRoom.CurrentTurn,
    });
  }
}