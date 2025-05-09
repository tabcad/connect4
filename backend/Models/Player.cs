namespace ConnectFour.Models;

public class Player
{
  public required Guid Id { get; set; }
  public required string Username { get; set; }
  public required int Wins { get; set; }
  public required int Losses { get; set; }
}
