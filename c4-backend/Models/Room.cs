namespace ConnectFour.Models;

public class Room
{
  public required int RoomId { get; set; }
  public required Guid PlayerOne { get; set; }
  public required Guid PlayerTwo { get; set; }
  public required bool IsActive { get; set; }
  public required DateTime CreatedOnUtc { get; set; }
  public required Guid CurrentTurn { get; set; }
  public required Guid GameId { get; set; }
  public Game? Game { get; set; }
}