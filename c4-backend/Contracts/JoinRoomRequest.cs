namespace ConnectFour.Contracts;

public class JoinRoomRequest
{
  public required Guid PlayerTwo { get; set; }
  public required Guid RoomId { get; set; }
}
