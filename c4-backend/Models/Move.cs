namespace ConnectFour.Models;
public class Move
{
  public required int MoveId { get; set; } // 1, 2, 3 etc
  public required Guid GameId { get; set; } // Guid of game being played
  public required Guid PlayerId { get; set; } // Which player's turn it is
  public required int Column { get; set; } // Chosen column for piece
  //add row
}