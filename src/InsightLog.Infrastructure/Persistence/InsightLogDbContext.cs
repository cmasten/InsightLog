using System.Reflection;

using InsightLog.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace InsightLog.Infrastructure.Persistence;

public class InsightLogDbContext : DbContext
{
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();

    public InsightLogDbContext(DbContextOptions<InsightLogDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
