using System.ComponentModel.DataAnnotations;

namespace InsightLog.Application.Dtos;

public class JournalEntryDto
{
    /// <summary>Unique identifier of the journal entry.</summary>
    public Guid Id { get; init; }

    /// <summary>ID of the user who owns this entry.</summary>
    [Required]
    public Guid UserId { get; init; }

    /// <summary>Date the entry was created.</summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>Freeform user text for the entry.</summary>
    [Required]
    [MaxLength(5000)]
    public string Content { get; init; } = string.Empty;

    /// <summary>List of mood tags for filtering or analysis.</summary>
    public List<string> MoodTags { get; init; } = new();

    /// <summary>Optional AI-generated summary of the content.</summary>
    public string? Summary { get; init; }
}
