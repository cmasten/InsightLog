using InsightLog.Domain.Events;

using MediatR;

namespace InsightLog.Tests.Integration;

public class FakeJournalEntryCreatedHandler : INotificationHandler<JournalEntryCreatedDomainEvent>
{
    public static List<Guid> FiredEvents = [];

    public Task Handle(JournalEntryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        FiredEvents.Add(notification.JournalEntryId.Value);
        return Task.CompletedTask;
    }
}
