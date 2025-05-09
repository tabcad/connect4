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
  public virtual DbSet<Player> Players { get; set; }
  public virtual DbSet<Move> Moves { get; set; }
  public virtual DbSet<Game> Games { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<Player>(e => 
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Username).IsRequired();
      e.Property(x => x.Wins).IsRequired().HasDefaultValue(0);
      e.Property(x => x.Losses).IsRequired().HasDefaultValue(0); 
    });

    modelBuilder.Entity<Game>(e => 
    {
      e.HasKey(x => x.GameId);

      e.Property(x => x.BoardState)
        .HasConversion(
          v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
          v => JsonSerializer.Deserialize<List<List<string?>>>(v, new JsonSerializerOptions()) ?? new List<List<string?>>()
        )
        .HasColumnType("TEXT"); // sqlite only supports text
      
      //fk to users
      e.HasOne<Player>().WithMany()
        .HasForeignKey(x => x.PlayerOne)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne<Player>().WithMany()
        .HasForeignKey(x => x.PlayerTwo)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne<Player>().WithMany()
        .HasForeignKey(x => x.CurrentTurn)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasOne<Player>().WithMany()
        .HasForeignKey(x => x.Winner)
        .OnDelete(DeleteBehavior.Restrict);

      e.HasMany<Move>().WithOne().HasForeignKey(y => y.GameId);
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
