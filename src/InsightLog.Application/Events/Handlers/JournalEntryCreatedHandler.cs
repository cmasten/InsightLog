using InsightLog.Application.Common;
using InsightLog.Domain.Common;
using InsightLog.Domain.Events;
using InsightLog.Domain.ValueObjects;

namespace InsightLog.Application.Events.Handlers;

public class JournalEntryCreatedHandler(
    IJournalEntryRepository journalEntryRepository,
    IUnitOfWork unitOfWork,
    IAISummaryGenerator summaryGenerator) : IDomainEventHandler<JournalEntryCreatedDomainEvent>
{
    public async Task Handle(JournalEntryCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var entry = await journalEntryRepository.GetByIdAsync(domainEvent.JournalEntryId, cancellationToken);

        if (entry is null || entry.Summary is not null)
        {
            return;
        }

        var summaryText = await summaryGenerator.GenerateSummaryAsync(entry.Content, cancellationToken);
        entry.SetAISummary(new AISummary(summaryText));

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
