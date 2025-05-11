using InsightLog.Application.Mappings;

namespace InsightLog.Application.Features.JournalEntries;

public static class CreateJournalEntry
{
    public record Command(
        UserId UserId,
        string Content,
        DateTime? CreatedDate,
        List<string>? MoodTags
    ) : IRequest<JournalEntryDto>;

    public class Handler(IJournalEntryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<Command, JournalEntryDto>
    {
        public async Task<JournalEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entry = new JournalEntry(
                userId: request.UserId,
                content: request.Content,
                createdDate: request.CreatedDate,
                moodTags: request.MoodTags
            );

            await repository.AddAsync(entry, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return entry.ToDto();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotNull().NotEqual(new UserId(default)).WithMessage("UserId is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Content).MaximumLength(5000);
        }
    }
}
