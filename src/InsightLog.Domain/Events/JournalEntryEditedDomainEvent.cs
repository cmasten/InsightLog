using InsightLog.Domain.Identifiers;
using InsightLog.Domain.Common;

namespace InsightLog.Domain.Events;

public class JournalEntryEditedDomainEvent(JournalEntryId JournalId) : DomainEventBase
{
    public JournalEntryId JournalId { get; } = JournalId;
}
