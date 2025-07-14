using ConnectFour.Contracts;
using ConnectFour.Exceptions;
using ConnectFour.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectFour.Services;

public class RoomServices
{
  private readonly AppDatabase db;
  private readonly GameServices gameService;

  public RoomServices(AppDatabase db, GameServices gameService)
  {
    this.db = db;
    this.gameService = gameService;
  }

  // internal async Task<Room> GetRoomByIdAsync(Guid id)
  // {
  //   var room = await db.Rooms
  //     .Include(x => x.Game) //need to make sure game is loaded first
  //     .FirstOrDefaultAsync(x => x.RoomId == id)
  //     ?? throw new NotFoundException($"Room with Id: {id} not found");

  //   return room;
  // }

  internal async Task<Room> GetRoomByIdAsync(Guid id)
  {
    var room = await db.Rooms
      .Include(r => r.Game)
        .ThenInclude(g => g.PlayerOne)
      .Include(r => r.Game)
        .ThenInclude(g => g.PlayerTwo)
      .Include(r => r.Game)
        .ThenInclude(g => g.CurrentTurn)
      .Include(r => r.Game)
        .ThenInclude(g => g.Winner)
      .FirstOrDefaultAsync(r => r.RoomId == id)
      ?? throw new NotFoundException($"Room with Id: {id} not found");

    return room;
  }

  internal async Task<List<RoomListResponse>> ListRoomsAsync()
  {
    var rooms = await db.Rooms
      .Select(x => new RoomListResponse { Id = x.RoomId })
      .ToListAsync();

    return rooms;
  }

  internal async Task<Room> CreateSinglePlayerRoomAsync(Guid playerOne, bool isSinglePlayer)
  {
    var bot = Guid.Parse("27d39162-1bad-44cb-8d76-75928917aedf");
    var game = await gameService.CreateNewGameAsync(playerOne, bot, isSinglePlayer);

    var room = new Room
    {
      RoomId = Guid.NewGuid(),
      Game = game,
      GameId = game.GameId,
      CreatedOnUtc = DateTime.UtcNow,
      IsActive = true
    };

    db.Rooms.Add(room);
    await db.SaveChangesAsync();

    return room;
  }

  internal async Task<Room> CreateRoomInviteAsync(Guid playerOne)
  {
    var game = await gameService.CreateNewGameAsync(playerOne, null, false);

    var newRoom = new Room
    {
      RoomId = Guid.NewGuid(),
      InviteLink = "link-to-room",
      Game = game,
      GameId = game.GameId,
      CreatedOnUtc = DateTime.UtcNow,
      IsActive = true
    };

    db.Rooms.Add(newRoom);
    await db.SaveChangesAsync();

    return newRoom;
  }

  // internal async Task<Room> JoinRoomAsync(Guid playerTwo, Guid roomId)
  // {
  //   var roomToJoin = await db.Rooms
  //     .Include(x => x.Game) // ensure game is loaded
  //     .FirstOrDefaultAsync(x => x.RoomId == roomId);

  //   await db.SaveChangesAsync();
  //   return roomToJoin;
  // }
}