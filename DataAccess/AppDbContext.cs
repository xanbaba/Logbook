using Logbook.DataAccess.Configurations;
using Logbook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logbook.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}