using _.Services;
using Microsoft.AspNetCore.Mvc;

namespace _.Controllers;

[Route("api/user")]
[ApiController]
public class UserControllers : ControllerBase
{
  private readonly AppDatabase db;
  private readonly UserServices services;

  public UserControllers(AppDatabase db, UserServices services)
  {
    this.db = db;
    this.services = services;
  }
}