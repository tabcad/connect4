namespace _.Services;

public class UserServices
{
  private readonly AppDatabase db;

  public UserServices(AppDatabase db)
  {
    this.db = db;
  }
}