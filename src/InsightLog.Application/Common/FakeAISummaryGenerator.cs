using InsightLog.Application.Common;

public class FakeAISummaryGenerator : IAISummaryGenerator
{
    public Task<string> GenerateSummaryAsync(string journalText, CancellationToken cancellationToken = default)
    {
        var summary = $"[AI Summary] First 10 words: {string.Join(" ", journalText.Split().Take(10))}";
        return Task.FromResult(summary);
    }
}
