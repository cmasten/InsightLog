using InsightLog.Application.Dtos;
namespace InsightLog.Blazor.Services;

public class JournalEntryService(HttpClient client)
{
    public async Task<List<JournalEntryDto>> GetEntriesAsync(Guid userId)
    {
        return await client.GetFromJsonAsync<List<JournalEntryDto>>($"api/v1/JournalEntries/user/{userId}")
               ?? [];
    }
}

