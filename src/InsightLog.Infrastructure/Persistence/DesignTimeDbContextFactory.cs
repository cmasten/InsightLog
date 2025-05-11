using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InsightLog.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InsightLogDbContext>
{
    public InsightLogDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<InsightLogDbContext>();
        optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));

        return new InsightLogDbContext(optionsBuilder.Options, null!);
    }
}
