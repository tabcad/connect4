namespace ConnectFour.Contracts;

public class GameResponse
{
  public required Guid GameId { get; set; }
  public required string?[][] Board { get; set; } = default!;
  public required Guid CurrentTurn { get; set; }
  public Guid? Winner { get; set; }
}