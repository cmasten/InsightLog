﻿using InsightLog.Domain.Identifiers;

namespace InsightLog.Domain.Events;

public class JournalEntryCreatedDomainEvent(JournalEntryId JournalId) : DomainEventBase
{
    public JournalEntryId JournalEntryId { get; } = JournalId;
}

