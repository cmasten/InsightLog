namespace InsightLog.Application.Interfaces;

public interface IJournalEntryRepository
{
    Task AddAsync(JournalEntry entry, CancellationToken cancellationToken);
    // TODO:
    // Task<JournalEntry?> GetByIdAsync(JournalEntryId id);
    Task<List<JournalEntry>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken);
}
