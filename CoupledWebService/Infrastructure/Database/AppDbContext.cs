using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<ServiceEntry> ServiceEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceEntry>()
            .Property(b => b.InsertedOn)
            .HasDefaultValueSql("now()");
    }
}
