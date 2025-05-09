namespace ConnectFour.Contracts;

public class LoginResponse
{
  public required string Token { get; set; }
  public required string Username { get; set; }
  public required Guid Id { get; set; }
}