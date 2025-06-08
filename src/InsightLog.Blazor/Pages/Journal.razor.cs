using InsightLog.Application.Dtos;

namespace InsightLog.Blazor.Pages;

public partial class Journal
{
    private List<JournalEntryDto> entries = new();
    private JournalEntryDto? selectedEntry;
    private Guid userId = Guid.Parse("de305d54-75b4-431b-adb2-eb6b9e546013"); // Replace with actual logged-in user ID

    protected override async Task OnInitializedAsync()
    {
        await LoadEntriesAsync();
    }

    private async Task LoadEntriesAsync()
    {
        try
        {
            var result = await Http.GetFromJsonAsync<List<JournalEntryDto>>($"/api/v1/journalentries/user/{userId}");
            if (result is not null)
            {
                entries = result;
                selectedEntry = entries.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching entries: {ex.Message}");
        }
    }

    private void SelectEntry(JournalEntryDto entry)
    {
        selectedEntry = entry;
    }

    private async Task OpenCreateEntryDialog()
    {
        var dialog = await DialogService.ShowAsync<JournalCreate>(null, new MudBlazor.DialogOptions
        {
            CloseButton = false,
            NoHeader = true,
            FullWidth = true,
        });
        var result = await dialog.Result;

        if (!result.Canceled && result.Data is JournalEntryDto newEntry)
        {
            entries.Insert(0, newEntry);
            selectedEntry = newEntry;
            await LoadEntriesAsync(); // Refresh entries to ensure the new entry is included

        }
    }
}
