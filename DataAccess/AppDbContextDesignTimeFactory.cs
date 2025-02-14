using Microsoft.EntityFrameworkCore.Design;

namespace Logbook.DataAccess;

// ReSharper disable once UnusedType.Global
public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        return new AppDbContext();
    }
}