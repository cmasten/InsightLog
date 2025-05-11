using FluentAssertions;

using InsightLog.Application.Features.JournalEntries;
using InsightLog.Application.Interfaces;
using InsightLog.Domain.Entities;
using InsightLog.Domain.Identifiers;

using Moq;

namespace InsightLog.Tests.Unit.Features.JournalEntries;

public class CreateJournalEntryTests
{
    private readonly Mock<IJournalEntryRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task Handler_Should_Create_Valid_Entry_And_Return_Dto()
    {
        // Arrange
        var command = new CreateJournalEntry.Command(
            new UserId(Guid.NewGuid()),
            "This is my test entry.",
            DateTime.UtcNow.Date,
            ["reflective", "focused"]
        );

        JournalEntry? savedEntry = null;

        _repoMock.Setup(r => r.AddAsync(It.IsAny<JournalEntry>(), It.IsAny<CancellationToken>()))
                 .Callback<JournalEntry, CancellationToken>((entry, _) => savedEntry = entry)
                 .Returns(Task.CompletedTask);

        var handler = new CreateJournalEntry.Handler(_repoMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Content.Should().Be(command.Content);
        result.MoodTags.Should().BeEquivalentTo(command.MoodTags);
        result.UserId.Should().Be(command.UserId.Value);
        result.Summary.Should().BeNull(); // Summary not generated on create
        savedEntry.Should().NotBeNull();
    }
}
