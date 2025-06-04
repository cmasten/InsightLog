namespace InsightLog.Application.Common;

public interface IAISummaryGenerator
{
    Task<string> GenerateSummaryAsync(string journalText, CancellationToken cancellationToken = default);
}
