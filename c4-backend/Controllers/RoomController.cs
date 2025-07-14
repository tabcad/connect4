using ConnectFour.Contracts;
using ConnectFour.Models;
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

  [HttpGet("{id:Guid}")]
  [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> GetRoomById([FromRoute] Guid id)
  {
    var room = await service.GetRoomByIdAsync(id);

    return Ok(new RoomResponse
    {
      RoomId = room.RoomId,
      Game = room.Game!,
      CreatedOnUtc = room.CreatedOnUtc,
      IsActive = room.IsActive,
    });
  }

  [HttpGet("all")]
  [ProducesResponseType(typeof(List<RoomListResponse>), StatusCodes.Status200OK)]
  public async Task<IActionResult> ListRooms()
  {
    var list = await service.ListRoomsAsync();

    return Ok(list);
  }

  [HttpPost("create")]
  public async Task<IActionResult> CreateNewRoom([FromBody] CreateRoomRequest request)
  {
    if (request.PlayerTwo == null && request.IsSinglePlayer)
    {
      var singlePlayerRoom = await service.CreateSinglePlayerRoomAsync(request.PlayerOne, request.IsSinglePlayer);

      return Ok(new RoomResponse
      {
        RoomId = singlePlayerRoom.RoomId,
        Game = singlePlayerRoom.Game!,
        CreatedOnUtc = singlePlayerRoom.CreatedOnUtc,
        IsActive = singlePlayerRoom.IsActive,
      });
    }
    else
    {
      var pendingRoom = await service.CreateRoomInviteAsync(request.PlayerOne);

      return Ok(new RoomInviteLinkResponse
      {
        RoomInviteLink = pendingRoom.InviteLink!,
        RoomId = pendingRoom.RoomId
      });
    }
  }

  // [HttpPost("join")]
  // public async Task<IActionResult> JoinRoom([FromBody] JoinRoomRequest request)
  // {
  //   //pass in p2 id to JoinRoomAsync(p2 id)
  //   //join room adds p2 to the game
  //   var room = await service.JoinRoomAsync(request.PlayerTwo, request.RoomId);

  //   return Ok(new RoomResponse
  //   {
  //     RoomId = room.RoomId,
  //     Game = room.Game!,
  //     CreatedOnUtc = room.CreatedOnUtc,
  //     IsActive = room.IsActive,
  //   });
  // }
}