using System.Data;
using ConnectFour.Contracts;
using ConnectFour.Exceptions;
using ConnectFour.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectFour.Services;

public class GameServices
{
  private readonly AppDatabase db;

  public GameServices(AppDatabase db)
  {
    this.db = db;
  }

  private static string?[][] CloneBoard(string?[][] board)
  {
    return board.Select(row => row.ToArray()).ToArray();
  }

  internal async Task<Game> CreateNewGameAsync(Guid playerOne, Guid playerTwo)
  {
    var game = new Game
    {
      GameId = Guid.NewGuid(),
      PlayerOne = playerOne,
      PlayerTwo = playerTwo,
      CurrentTurn = playerOne,
      Winner = null,
      Board = Game.CreateEmptyBoard(),
    };

    db.Games.Add(game);
    await db.SaveChangesAsync();

    return game;
  }

  internal async Task<List<AllGamesResponse>> ListAllGamesAsync()
  {
    var games = await db.Games.ToListAsync();
    var response = games.Select(x => new AllGamesResponse
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

  internal async Task<GameResponse> GameTurnAsync(Guid playerId, Guid gameId, int col)
  {
    var game = await db.Games.FirstOrDefaultAsync(x => x.GameId == gameId)
      ?? throw new NotFoundException($"Game with Id: {gameId} not found");

    if (game.CurrentTurn != playerId)
      throw new InvalidOperationException("Not this player's turn");

    var board = game.Board;
    int row = -1;

    for (int r = board.Length - 1; r >= 0; r--)
    {
      if (board[r][col] == null)
      {
        row = r;
        break;
      }
    }

    if (row == -1)
      throw new InvalidOperationException("Column is full");

    var newBoard = CloneBoard(board);
    newBoard[row][col] = playerId.ToString();
    game.Board = newBoard;
    game.CurrentTurn = (playerId == game.PlayerOne) ? game.PlayerTwo : game.PlayerOne;

    await db.SaveChangesAsync();

    return new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurn,
      Board = game.Board
    };
  }

  internal async Task<GameResponse> RestartCurrentGameAsync(Guid gameId)
  {
    var game = await db.Games.FirstAsync(x => x.GameId == gameId);

    game.Board = Game.CreateEmptyBoard();
    await db.SaveChangesAsync();

    return new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurn,
      Board = game.Board
    };
  }

  // public static string WinningCombinations(int row, int col)
  // {
  //   var directions = [
  //     [0, 1], // right
  //     [1, 0], // left
  //     [1, 1], // diagonal right
  //     [1, -1] // diagonal left
  //   ];

  //   foreach (var item in collection)
  //   {

  //   }
  // }
}