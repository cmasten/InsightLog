using InsightLog.Domain.Events;
using InsightLog.Domain.Identifiers;

namespace InsightLog.Domain.Entities;

public class JournalEntry : Entity, IAggregateRoot
{
    public JournalEntryId Id { get; set; }
    public UserId UserId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string Content { get; private set; }
    public List<string> MoodTags { get; private set; }
    public AISummary? Summary { get; private set; }
    public bool IsDeleted { get; private set; }

    // EF Core requires a parameterless constructor
    private JournalEntry()
    {
        Id = default;
        UserId = default;
        CreatedDate = default;
        Content = default!;
        MoodTags = [];
        Summary = null;
        IsDeleted = false;
    }

    public JournalEntry(UserId userId, string content, DateTime? createdDate = null, List<string>? moodTags = null)
    {
        Id = new JournalEntryId(Guid.NewGuid());

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Journal entry content must not be empty.", nameof(content));
        }

        UserId = userId;
        Content = content.Trim();
        CreatedDate = (createdDate ?? DateTime.UtcNow).Date;

        if (CreatedDate > DateTime.UtcNow.Date)
        {
            throw new InvalidOperationException("Journal entry date cannot be in the future.");
        }

        MoodTags = moodTags ?? new List<string>();
        IsDeleted = false;

        AddDomainEvent(new JournalEntryCreatedDomainEvent(Id));
    }

    public void Edit(string newContent, List<string>? newTags = null)
    {
        if (string.IsNullOrWhiteSpace(newContent))
        {
            throw new ArgumentException("Journal entry content must not be empty.", nameof(newContent));
        }

        Content = newContent.Trim();
        MoodTags = newTags ?? MoodTags;
        Summary = null; // Invalidate existing summary

        AddDomainEvent(new JournalEntryEditedDomainEvent(Id));
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void SetAISummary(AISummary summary)
    {
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
    }
}
