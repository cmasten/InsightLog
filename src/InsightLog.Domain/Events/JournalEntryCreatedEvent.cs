using InsightLog.Domain.Common;
using InsightLog.Domain.Entities;

namespace InsightLog.Domain.Events;

public record JournalEntryCreatedEvent(JournalEntry Entry) : IDomainEvent;

