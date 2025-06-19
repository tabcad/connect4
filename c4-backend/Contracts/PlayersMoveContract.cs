using Microsoft.AspNetCore.Routing.Constraints;

namespace ConnectFour.Contracts;

public class PlayersMoveContract
{
  public required Guid PlayerId { get; set; }
  public required Guid GameId { get; set; }
  public required int Col { get; set; }
}