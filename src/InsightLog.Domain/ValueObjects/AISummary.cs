namespace InsightLog.Domain.ValueObjects;

public class AISummary
{
    public string SummaryText { get; private set; }
    public DateTime GeneratedAt { get; private set; }

    private AISummary()
    {
        SummaryText = default!;
        GeneratedAt = default;
    }

    public AISummary(string summaryText, DateTime? generatedAt = null)
    {
        if (string.IsNullOrWhiteSpace(summaryText))
            throw new ArgumentException("AI summary text must not be empty.", nameof(summaryText));

        SummaryText = summaryText.Trim();
        GeneratedAt = generatedAt ?? DateTime.UtcNow;
    }
}
