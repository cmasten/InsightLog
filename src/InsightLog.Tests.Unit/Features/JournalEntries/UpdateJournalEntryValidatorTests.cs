using FluentValidation.TestHelper;

using InsightLog.Application.Features.JournalEntries;

namespace InsightLog.Tests.Unit.Features.JournalEntries;

public class UpdateJournalEntryValidatorTests
{
    private readonly UpdateJournalEntry.Validator _validator = new();

    [Fact]
    public void Should_Fail_When_Content_Is_Empty()
    {
        var command = new UpdateJournalEntry.Command(Guid.NewGuid(), string.Empty, null);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public void Should_Fail_When_Id_Is_Empty()
    {
        var command = new UpdateJournalEntry.Command(Guid.Empty, "text", null);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
