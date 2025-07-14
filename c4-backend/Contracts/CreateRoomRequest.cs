namespace ConnectFour.Contracts;

public class CreateRoomRequest
{
  public Guid PlayerOne { get; set; }
  public Guid? PlayerTwo { get; set; }
  public bool IsSinglePlayer { get; set; }
}
