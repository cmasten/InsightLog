using InsightLog.Application.Mappings;

namespace InsightLog.Application.Features.JournalEntries;

public static class UpdateJournalEntry
{
    public record Command(
        Guid Id,
        string Content,
        List<string>? MoodTags
    ) : IRequest<JournalEntryDto?>;

    public class Handler(IJournalEntryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<Command, JournalEntryDto?>
    {
        public async Task<JournalEntryDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var entry = await repository.GetByIdAsync(new JournalEntryId(request.Id), cancellationToken);
            if (entry is null || entry.IsDeleted)
            {
                return null;
            }

            entry.Edit(request.Content, request.MoodTags);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return entry.ToDto();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Content).MaximumLength(5000);
        }
    }
}
