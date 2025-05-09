using System.ComponentModel.DataAnnotations;

namespace ConnectFour.Contracts;

public class CreateUserContract
{
	[MinLength(1), MaxLength(64)]
	public required string Username { get; set; }
  public required string Password { get; set; }
}