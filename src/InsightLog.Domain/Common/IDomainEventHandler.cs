using MediatR;

namespace InsightLog.Domain.Common;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}
