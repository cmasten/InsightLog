namespace InsightLog.Application.Interfaces;

public interface IJournalEntryRepository
{
    Task AddAsync(JournalEntry entry, CancellationToken cancellationToken);
    Task<JournalEntry?> GetByIdAsync(JournalEntryId id, CancellationToken cancellationToken);
    Task<List<JournalEntry>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken);
}
