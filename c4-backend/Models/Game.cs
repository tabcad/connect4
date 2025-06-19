using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectFour.Models;

public class Game
{
  public required Guid GameId { get; set; }
  public required Guid PlayerOne { get; set; }
  public required Guid PlayerTwo { get; set; }
  public required Guid CurrentTurn { get; set; }
  public Guid? Winner { get; set; }
  public string?[][] Board { get; set; } = CreateEmptyBoard();

  //game logic
  [NotMapped]
  public string?[,] GameLogicBoard
  {
    get => ConvertTo2D(Board); //get the board from the db
    set => Board = ConvertToJagged(value); //convert to jagged array for implementing win logic
  }

  public const int Rows = 6;
  public const int Columns = 7;
  public static string?[][] CreateEmptyBoard()
  {
    var board = new string?[Rows][];
    for (int i = 0; i < Rows; i++)
    {
      board[i] = new string?[Columns];
    }
    return board;
  }

  public static string?[,] ConvertTo2D(string?[][] jagged)
  {
    int rows = jagged.Length;
    int cols = jagged[0].Length;
    var grid = new string?[rows, cols];
    for (int i = 0; i < rows; i++)
      for (int m = 0; m < cols; m++)
        grid[i, m] = jagged[i][m];
    return grid;
  }

  public static string?[][] ConvertToJagged(string?[,] array)
  {
    int rows = array.GetLength(0);
    int cols = array.GetLength(1);
    var jagged = new string?[rows][];
    for (int i = 0; i < rows; i++)
    {
      jagged[i] = new string?[cols];
      for (int m = 0; m < cols; m++)
      {
        jagged[i][m] = array[i, m];
      }
    }
    return jagged;
  }
}

// multidimensional arrays aka 2D arrays, [,], are easier for working out win logic but they aren't directly
// serializable by ef core via json
//persist the board as a jagged array, [][], in the db for json compatibility and ef core storage