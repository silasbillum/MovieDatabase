using Microsoft.EntityFrameworkCore;
using Models;

namespace MovieDatabase.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<WatchlistEntry> WatchlistEntries { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Movie entity
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MovieTitle).HasMaxLength(500);
            entity.Property(e => e.Overview).HasMaxLength(2000);
            entity.Property(e => e.PosterPath).HasMaxLength(500);
            entity.Property(e => e.ReleaseDate).HasMaxLength(50);
            entity.Property(e => e.Language).HasMaxLength(10);
        });

        // Configure WatchlistEntry entity
        modelBuilder.Entity<WatchlistEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Movie)
                  .WithMany()
                  .HasForeignKey(e => e.MovieId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Genre entity
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);
        });
    }
}