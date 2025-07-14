using ConnectFour.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class AppDatabase : IdentityDbContext<IdentityUser>
{
  public AppDatabase(DbContextOptions<AppDatabase> options)
    : base(options)
  {
  }
  public virtual DbSet<Room> Rooms { get; set; }
  public virtual DbSet<Game> Games { get; set; }
  public virtual DbSet<Player> Players { get; set; }
  public virtual DbSet<Move> Moves { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Room>(e =>
    {
      e.HasKey(x => x.RoomId);

      e.Property(x => x.RoomId).IsRequired();
      e.Property(x => x.InviteLink);
      e.Property(x => x.GameId).IsRequired();
      e.Property(x => x.IsActive).IsRequired();
      e.Property(x => x.CreatedOnUtc).IsRequired();

      e.HasOne(x => x.Game)
        .WithMany()
        .HasForeignKey(x => x.GameId)
        .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Game>(e =>
    {
      e.HasKey(x => x.GameId);
      e.Property(x => x.IsSinglePlayer).IsRequired();
      e.Property(x => x.Board)
        .HasConversion(
          v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
          v => JsonSerializer.Deserialize<string?[][]>(v, (JsonSerializerOptions?)null)!
        )
        .HasColumnType("TEXT");

      e.HasOne(x => x.PlayerOne)
        .WithMany()
        .HasForeignKey(x => x.PlayerOneId)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne(x => x.PlayerTwo)
        .WithMany()
        .HasForeignKey(x => x.PlayerTwoId)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne(g => g.CurrentTurn)
        .WithMany()
        .HasForeignKey(g => g.CurrentTurnId)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne(g => g.Winner)
        .WithMany()
        .HasForeignKey(g => g.WinnerId)
        .OnDelete(DeleteBehavior.Restrict);


      e.HasMany<Move>().WithOne().HasForeignKey(y => y.GameId);
    });

    modelBuilder.Entity<Player>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Username).IsRequired();
      e.Property(x => x.Wins).IsRequired().HasDefaultValue(0);
      e.Property(x => x.Losses).IsRequired().HasDefaultValue(0);

      e.HasIndex(e => e.Username).IsUnique();
    });

    modelBuilder.Entity<Move>(e =>
    {
      e.HasKey(x => x.MoveId);

      e.HasOne<Player>().WithMany()
        .HasForeignKey(x => x.PlayerId)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne<Game>().WithMany()
        .HasForeignKey(x => x.GameId)
        .OnDelete(DeleteBehavior.Cascade);
    });
  }
}
