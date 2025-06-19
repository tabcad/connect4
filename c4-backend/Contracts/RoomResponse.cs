using ConnectFour.Models;

namespace ConnectFour.Contracts;

public class RoomResponse
{
  public required int RoomId { get; set; }
  public required Guid PlayerOne { get; set; }
  public required Guid PlayerTwo { get; set; }
  public required Guid GameId { get; set; }
  public required Game Game { get; set; }
  public required DateTime CreatedOnUtc { get; set; }
  public required bool IsActive { get; set; }
  public required Guid CurrentTurn { get; set; }
}