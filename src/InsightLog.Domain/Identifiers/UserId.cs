namespace InsightLog.Domain.Identifiers;

public readonly record struct UserId(Guid Value) : IStronglyTypedId<Guid>;
