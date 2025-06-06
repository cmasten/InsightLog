using InsightLog.Application.Dtos;
using InsightLog.Application.Features.JournalEntries;
namespace InsightLog.Blazor.Services;

public class JournalEntryService(HttpClient client)
{
    public async Task<List<JournalEntryDto>> GetEntriesAsync(Guid userId)
    {
        return await client.GetFromJsonAsync<List<JournalEntryDto>>($"api/v1/JournalEntries/user/{userId}")
               ?? [];
    }

    public async Task<JournalEntryDto?> CreateAsync(CreateJournalEntry.Command command)
    {
        var response = await client.PostAsJsonAsync("api/v1/journalentries", command);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JournalEntryDto>();
    }
}

