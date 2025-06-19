using System.Data;
using ConnectFour.Contracts;
using ConnectFour.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectFour.Services;

public class RoomServices
{
  private readonly AppDatabase db;
  private readonly GameServices service;

  public RoomServices(AppDatabase db, GameServices service)
  {
    this.db = db;
    this.service = service;
  }

  internal async Task<List<AllGamesResponse>> ListRoomsAsync()
  {
    var rooms = await db.Rooms.ToListAsync();
    var response = rooms.Select(x => new AllGamesResponse
    {
      GameId = x.GameId,
    }).ToList();

    return response;
  }

  internal async Task<Game> GetGameByIdAsync(Guid id)
  {
    var game = await db.Games.FirstAsync(x => x.GameId == id);
    return game;
  }

  internal async Task CreateSinglePlayerRoomAsync(Guid playerOne)
  {
    var bot = Guid.NewGuid();
    var game = await service.CreateNewGameAsync(playerOne, bot);

    db.Rooms.Add(new Room
    {
      RoomId = 1,
      PlayerOne = game.PlayerOne,
      PlayerTwo = game.PlayerTwo,
      Game = game,
      GameId = game.GameId,
      CreatedOnUtc = DateTime.UtcNow,
      IsActive = true,
      CurrentTurn = game.CurrentTurn
    });


  }
  internal async Task<Room> CreateTwoPlayerRoomAsync(Guid playerOne, Guid playerTwo)
  {
    var game = await service.CreateNewGameAsync(playerOne, playerTwo);

    var room = new Room
    {
      RoomId = 1,
      PlayerOne = game.PlayerOne,
      PlayerTwo = game.PlayerTwo,
      Game = game,
      GameId = game.GameId,
      CreatedOnUtc = DateTime.UtcNow,
      IsActive = true,
      CurrentTurn = game.CurrentTurn
    };

    db.Rooms.Add(room);
    await db.SaveChangesAsync();
    return room;
  }

}