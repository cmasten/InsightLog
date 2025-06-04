using InsightLog.Domain.Common;
using InsightLog.Domain.Events;

namespace InsightLog.Application.Events.Handlers;

public class JournalEntryEditedHandler : IDomainEventHandler<JournalEntryEditedDomainEvent>
{
    public Task HandleAsync(JournalEntryEditedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // TODO: actions after edit
        Console.WriteLine($"✏️ Journal entry edited: {domainEvent.JournalId}");
        return Task.CompletedTask;
    }
}
