using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace MovieDatabase
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<WatchlistEntry> watchlistEntries { get; set; }
    }
}
