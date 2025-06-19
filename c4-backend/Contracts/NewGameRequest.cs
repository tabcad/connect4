namespace ConnectFour.Contracts;

public class NewGameRequest
{
  public required Guid PlayerOne { get; set; }
  public required Guid PlayerTwo { get; set; }
}