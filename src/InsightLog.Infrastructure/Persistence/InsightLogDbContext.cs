using System.Reflection;

using InsightLog.Domain.Entities;
using InsightLog.Infrastructure.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace InsightLog.Infrastructure.Persistence;

public class InsightLogDbContext(
    DbContextOptions<InsightLogDbContext> options,
    IMediator mediator) : DbContext(options)
{
    private readonly IMediator _mediator = mediator;

    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
