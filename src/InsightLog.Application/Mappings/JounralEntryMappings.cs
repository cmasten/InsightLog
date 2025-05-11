using InsightLog.Application.Dtos;
using InsightLog.Domain.Entities;

namespace InsightLog.Application.Mappings;

public static class JournalEntryMappings
{
    public static JournalEntryDto ToDto(this JournalEntry entry)
    {
        return new JournalEntryDto
        {
            Id = entry.Id.Value,
            UserId = entry.UserId.Value,
            CreatedDate = entry.CreatedDate,
            Content = entry.Content,
            MoodTags = entry.MoodTags,
            Summary = entry.Summary?.SummaryText
        };
    }
}
