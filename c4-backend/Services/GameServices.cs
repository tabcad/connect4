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

  internal async Task<Game> CreateNewGameAsync(Guid playerOne, Guid? playerTwo, bool isSinglePlayer)
  {
    var game = new Game
    {
      GameId = Guid.NewGuid(),
      PlayerOneId = playerOne,
      PlayerTwoId = playerTwo,
      IsSinglePlayer = isSinglePlayer,
      CurrentTurnId = playerOne,
      WinnerId = null,
      Board = Game.CreateEmptyBoard(),
    };

    db.Games.Add(game);
    await db.SaveChangesAsync();

    return await db.Games
      .Include(x => x.PlayerOne)
      .Include(x => x.PlayerTwo)
      .Include(g => g.CurrentTurn)
      .Include(g => g.Winner)
      .FirstAsync(g => g.GameId == game.GameId);
  }

  internal async Task<GameResponse> RestartCurrentGameAsync(Guid gameId)
  {
    var game = await db.Games.FirstAsync(x => x.GameId == gameId);
    var isSinglePlayer = game.IsSinglePlayer;

    game.CurrentTurnId = game.PlayerOneId;
    game.Winner = null;
    game.WinnerId = null;
    game.IsSinglePlayer = isSinglePlayer;
    game.Board = Game.CreateEmptyBoard();
    await db.SaveChangesAsync();

    return new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurnId,
      Board = game.Board,
      Winner = game.WinnerId
    };
  }

  internal async Task<GameResponse> GameTurnAsync(Guid playerId, Guid gameId, int col)
  {
    var game = await db.Games.FirstOrDefaultAsync(x => x.GameId == gameId)
      ?? throw new NotFoundException($"Game with Id: {gameId} not found");

    if (game.CurrentTurnId != playerId)
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

    if (CheckForWin(newBoard, playerId.ToString()))
    {
      game.WinnerId = playerId;
    }
    else
    {
      game.CurrentTurnId = (playerId == game.PlayerOneId) ? game.PlayerTwoId!.Value : game.PlayerOneId;
    }

    await db.SaveChangesAsync();

    return new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurnId,
      Board = game.Board,
      Winner = game.WinnerId
    };
  }

  internal async Task<GameResponse> BotTurnAsync(Guid gameId)
  {
    var game = await db.Games.FirstOrDefaultAsync(x => x.GameId == gameId)
        ?? throw new NotFoundException("Game not found");

    if (!game.IsSinglePlayer || game.CurrentTurnId != game.PlayerTwoId)
      throw new InvalidOperationException("Not bot's turn");

    BotTurn(game);

    await db.SaveChangesAsync();

    return new GameResponse
    {
      GameId = game.GameId,
      CurrentTurn = game.CurrentTurnId,
      Board = game.Board
    };
  }

  private void BotTurn(Game game)
  {
    var botId = game.PlayerTwoId!.Value;
    var newBoard = CloneBoard(game.Board);

    for (int col = 0; col < Game.Columns; col++)
    {
      for (int row = Game.Rows - 1; row >= 0; row--)
      {
        if (newBoard[row][col] == null)
        {
          newBoard[row][col] = botId.ToString();
          game.Board = newBoard;

          if (CheckForWin(newBoard, botId.ToString()))
          {
            game.WinnerId = botId;
          }
          else
          {
            game.CurrentTurnId = game.PlayerOneId;
          }

          return;
        }
      }
    }
    throw new InvalidOperationException("Bot can't make a move");
  }

  public static bool CheckForWin(string?[][] board, string playerId)
  {
    int rows = board.Length;
    int cols = board[0].Length;

    bool HasFour(int rStep, int cStep)
    {
      for (int row = 0; row < rows; row++)
      {
        for (int col = 0; col < cols; col++)
        {
          int count = 0;
          for (int i = 0; i < 4; i++)
          {
            int r = row + rStep * i;
            int c = col + cStep * i;

            if (r < 0 || r >= rows || c < 0 || c >= cols)
              break;

            if (board[r][c] == playerId)
              count++;
            else
              break;
          }

          if (count == 4)
            return true;
        }
      }
      return false;
    }

    return HasFour(0, 1)
        || HasFour(1, 0)
        || HasFour(1, 1)
        || HasFour(-1, 1);
  }
}