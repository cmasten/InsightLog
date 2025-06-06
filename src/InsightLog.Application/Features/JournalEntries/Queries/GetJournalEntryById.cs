using InsightLog.Application.Mappings;

namespace InsightLog.Application.Features.JournalEntries.Queries;

public static class GetJournalEntryById
{
    public record Query(Guid Id) : IRequest<JournalEntryDto?>;

    public class Handler(IJournalEntryRepository repository) : IRequestHandler<Query, JournalEntryDto?>
    {
        public async Task<JournalEntryDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var entry = await repository.GetByIdAsync(new JournalEntryId(request.Id), cancellationToken);
            return entry?.ToDto();
        }
    }
}
