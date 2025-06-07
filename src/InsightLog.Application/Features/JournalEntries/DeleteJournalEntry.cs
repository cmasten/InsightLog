namespace InsightLog.Application.Features.JournalEntries;

public static class DeleteJournalEntry
{
    public record Command(Guid Id) : IRequest<bool>;

    public class Handler(IJournalEntryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var entry = await repository.GetByIdAsync(new JournalEntryId(request.Id), cancellationToken);
            if (entry is null || entry.IsDeleted)
            {
                return false;
            }

            entry.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
}
