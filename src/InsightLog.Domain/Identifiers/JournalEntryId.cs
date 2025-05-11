namespace InsightLog.Domain.Identifiers;

public readonly record struct JournalEntryId(Guid Value) : IStronglyTypedId<Guid>;
