using InsightLog.Application.Interfaces;
using InsightLog.Domain.Entities;
using InsightLog.Domain.Identifiers;

using Microsoft.EntityFrameworkCore;

namespace InsightLog.Infrastructure.Persistence.Repositories;

public class JournalEntryRepository(InsightLogDbContext context) : IJournalEntryRepository
{
    public async Task AddAsync(JournalEntry entry, CancellationToken cancellationToken)
    {
        await context.JournalEntries.AddAsync(entry, cancellationToken);
    }

    public async Task<JournalEntry?> GetByIdAsync(JournalEntryId id, CancellationToken cancellationToken)
    {
        return await context.JournalEntries.FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
    }

    public Task UpdateAsync(JournalEntry entry, CancellationToken cancellationToken)
    {
        context.JournalEntries.Update(entry);
        return Task.CompletedTask;
    }
    public async Task<List<JournalEntry>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await context.JournalEntries
            .Where(j => j.UserId == userId)
            .ToListAsync(cancellationToken);
    }

}
