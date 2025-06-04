using InsightLog.Application.Mappings;

namespace InsightLog.Application.Features.JournalEntries;

public static class EditJournalEntry
{
    public record Command(
        JournalEntryId Id,
        string Content,
        List<string>? MoodTags
    ) : IRequest<JournalEntryDto>;

    public class Handler(IJournalEntryRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<Command, JournalEntryDto>
    {
        public async Task<JournalEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entry = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (entry is null)
            {
                throw new KeyNotFoundException("Journal entry not found");
            }

            entry.Edit(request.Content, request.MoodTags);

            await repository.UpdateAsync(entry, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return entry.ToDto();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(new JournalEntryId(default));
            RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
        }
    }
}
