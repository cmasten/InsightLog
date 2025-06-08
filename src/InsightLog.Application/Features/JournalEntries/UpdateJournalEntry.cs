using InsightLog.Application.Mappings;
using InsightLog.Application.Dtos;
using InsightLog.Domain.Identifiers;
using InsightLog.Application.Interfaces;
using FluentValidation;
using MediatR;

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
            if (entry is null)
                return null;

            entry.Edit(request.Content, request.MoodTags);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return entry.ToDto();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Id is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Content).MaximumLength(5000);
        }
    }
}
