namespace ConnectFour.Contracts;

public class AccountResponse
{
  public required Guid Id { get; set; }
  public required string Username { get; set; }
  public required int Wins { get; set; }
  public required int Losses { get; set; }
}