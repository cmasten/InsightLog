using InsightLog.Application.Dtos;
using InsightLog.Application.Mappings;

namespace InsightLog.Application.Features.JournalEntries;

public static class GetJournalEntries
{
    public record Query(UserId UserId) : IRequest<List<JournalEntryDto>>;

    public class Handler(IJournalEntryRepository repository) : IRequestHandler<Query, List<JournalEntryDto>>
    {
        public async Task<List<JournalEntryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var entries = await repository.GetByUserIdAsync(request.UserId, cancellationToken);

            return [.. entries.Select(e => e.ToDto())];
        }
    }
}
