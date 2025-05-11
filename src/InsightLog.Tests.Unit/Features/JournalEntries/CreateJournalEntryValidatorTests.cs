using FluentValidation.TestHelper;

using InsightLog.Application.Features.JournalEntries;
using InsightLog.Domain.Identifiers;

namespace InsightLog.Tests.Unit.Features.JournalEntries;

public class CreateJournalEntryValidatorTests
{
    private readonly CreateJournalEntry.Validator _validator = new();

    [Fact]
    public void Should_Pass_Validation_For_Valid_Command()
    {
        var command = new CreateJournalEntry.Command(
            UserId: new UserId(Guid.NewGuid()),
            Content: "This is a valid journal entry.",
            CreatedDate: DateTime.UtcNow,
            MoodTags: ["focused", "motivated"]
        );

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_Content_Is_Missing()
    {
        var command = new CreateJournalEntry.Command(
            UserId: new UserId(Guid.NewGuid()),
            Content: "",
            CreatedDate: DateTime.UtcNow,
            MoodTags: null
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public void Should_Fail_When_UserId_Is_Default()
    {
        var command = new CreateJournalEntry.Command(
            UserId: default,
            Content: "Valid content",
            CreatedDate: DateTime.UtcNow,
            MoodTags: null
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Fact]
    public void Should_Fail_When_Content_Exceeds_Length()
    {
        var command = new CreateJournalEntry.Command(
            UserId: new UserId(Guid.NewGuid()),
            Content: new string('x', 5001),
            CreatedDate: DateTime.UtcNow,
            MoodTags: null
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Content);
    }
}
