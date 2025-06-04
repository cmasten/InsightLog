using System.Net.Http.Json;

using FluentAssertions;

using InsightLog.Application.Dtos;
using InsightLog.Application.Features.JournalEntries;
using InsightLog.Domain.Identifiers;

namespace InsightLog.Tests.Integration;

public class JournalEntryEndpointTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    private static string _baseUrl => "/api/v1/journalentries";

    [Fact]
    public async Task CreateJournalEntry_ShouldReturnCreatedEntry()
    {
        var command = new CreateJournalEntry.Command(
            new UserId(Guid.NewGuid()),
            "Integration test entry",
            DateTime.UtcNow,
            []);

        var response = await _client.PostAsJsonAsync(_baseUrl, command);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<JournalEntryDto>();
        dto.Should().NotBeNull();
        dto!.Content.Should().Be("Integration test entry");
    }

    [Fact]
    public async Task CreatingJournalEntry_Should_Fire_JournalEntryCreatedDomainEvent()
    {
        // Arrange
        FakeJournalEntryCreatedHandler.FiredEvents.Clear();

        var command = new CreateJournalEntry.Command(
            new UserId(Guid.NewGuid()),
            "Integration test entry",
            DateTime.UtcNow,
            []);

        // Act
        var response = await _client.PostAsJsonAsync(_baseUrl, command);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<JournalEntryDto>();

        // Assert
        FakeJournalEntryCreatedHandler.FiredEvents.Should().ContainSingle();
        FakeJournalEntryCreatedHandler.FiredEvents.Single().Should().Be(dto!.Id);
    }

    [Fact]
    public async Task EditJournalEntry_Should_Update_Entry()
    {
        var create = new CreateJournalEntry.Command(new UserId(Guid.NewGuid()), "Before edit", DateTime.UtcNow, []);

        var createResponse = await _client.PostAsJsonAsync(_baseUrl, create);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<JournalEntryDto>();

        var editCommand = new EditJournalEntry.Command(default, "After edit", ["relaxed"]);
        var editResponse = await _client.PutAsJsonAsync($"{_baseUrl}/{created!.Id}", editCommand);
        editResponse.EnsureSuccessStatusCode();

        var list = await _client.GetFromJsonAsync<List<JournalEntryDto>>($"{_baseUrl}/user/{created.UserId}");

        list.Should().ContainSingle();
        list![0].Content.Should().Be("After edit");
        list[0].MoodTags.Should().Contain("relaxed");
    }

}
