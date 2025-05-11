using InsightLog.Domain.Events;

using MediatR;

namespace InsightLog.Tests.Integration;

public class FakeJournalEntryCreatedHandler : INotificationHandler<JournalEntryCreatedDomainEvent>
{
    public static List<Guid> FiredEvents = [];

    public Task Handle(JournalEntryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        FiredEvents.Add(notification.JournalId.Value);
        return Task.CompletedTask;
    }
}
