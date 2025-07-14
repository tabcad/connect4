namespace ConnectFour.Contracts;

public class RoomInviteLinkResponse
{
  public required string RoomInviteLink { get; set; }
  public required Guid RoomId { get; set; }
}