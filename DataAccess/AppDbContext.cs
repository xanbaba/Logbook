using Logbook.DataAccess.Configurations;
using Logbook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logbook.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<GroupTeacher> GroupTeachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new GroupTeacherConfiguration());
    }
}