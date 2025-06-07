using FluentAssertions;

using InsightLog.Application.Features.JournalEntries;
using InsightLog.Application.Interfaces;
using InsightLog.Domain.Entities;
using InsightLog.Domain.Identifiers;

using Moq;

namespace InsightLog.Tests.Unit.Features.JournalEntries;

public class UpdateJournalEntryTests
{
    private readonly Mock<IJournalEntryRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task Handler_Should_Update_Entry_And_Return_Dto()
    {
        var entry = new JournalEntry(new UserId(Guid.NewGuid()), "orig");
        _repoMock.Setup(r => r.GetByIdAsync(entry.Id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(entry);

        var command = new UpdateJournalEntry.Command(entry.Id.Value, "updated", ["tag1"]);

        var handler = new UpdateJournalEntry.Handler(_repoMock.Object, _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.Should().NotBeNull();
        result!.Content.Should().Be("updated");
        entry.Content.Should().Be("updated");
    }
}
