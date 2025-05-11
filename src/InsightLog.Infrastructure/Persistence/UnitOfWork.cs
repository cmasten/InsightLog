using InsightLog.Application.Interfaces;

namespace InsightLog.Infrastructure.Persistence;

public class UnitOfWork(InsightLogDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveEntitiesAsync(cancellationToken);
    }
}
