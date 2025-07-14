using ConnectFour.Models;

namespace ConnectFour.Contracts;

public class RoomResponse
{
  public required Guid RoomId { get; set; }
  public required Game Game { get; set; }
  public required DateTime CreatedOnUtc { get; set; }
  public required bool IsActive { get; set; }
}