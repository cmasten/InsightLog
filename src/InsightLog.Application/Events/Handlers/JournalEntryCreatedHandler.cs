using InsightLog.Domain.Common;
using InsightLog.Domain.Events;

namespace InsightLog.Application.Events.Handlers;

public class JournalEntryCreatedHandler : IDomainEventHandler<JournalEntryCreatedDomainEvent>
{
    public Task HandleAsync(JournalEntryCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // TODO: trigger AI summary generator or background queue
        Console.WriteLine($"📌 Journal entry created: {domainEvent.JournalId}");
        return Task.CompletedTask;
    }
}
