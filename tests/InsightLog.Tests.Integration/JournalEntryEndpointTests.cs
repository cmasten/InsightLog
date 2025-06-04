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

}
