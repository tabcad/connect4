namespace _.Models;
public class Game
{
  public required Guid GameId { get; set; }
  public required Guid PlayerOne { get; set; }
  public required Guid PlayerTwo { get; set; }
  public required Guid CurrentTurn { get; set; }
  //string[,]
  public List<List<string?>> BoardState { get; set; } =
  [
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
    [null, null, null, null, null, null],
  ];
  public Guid? Winner { get; set; }
}