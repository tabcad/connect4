namespace ConnectFour.Models;

public class Room
{
  public required Guid RoomId { get; set; }
  public string? InviteLink { get; set; }
  public required bool IsActive { get; set; }
  public required DateTime CreatedOnUtc { get; set; }
  public required Guid GameId { get; set; }
  public required Game Game { get; set; } = default!;
}