using InsightLog.Domain.Entities;
using InsightLog.Domain.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightLog.Infrastructure.Persistence.Configurations;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .HasConversion(
                id => id.Value,
                value => new JournalEntryId(value)
            );

        builder.Property(j => j.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(j => j.CreatedDate)
            .IsRequired();

        builder.Property(j => j.UserId)
            .HasConversion(
                id => id.ToString(),
                value => new UserId(Guid.Parse(value)))
            .IsRequired();

        builder.Property(j => j.IsDeleted)
            .IsRequired();

        builder.Property(j => j.MoodTags)
            .HasConversion(
                tags => string.Join(',', tags),
                tags => tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.OwnsOne(j => j.Summary, summary =>
        {
            summary.Property(s => s.SummaryText).HasColumnName("AISummaryText").HasMaxLength(2000);
            summary.Property(s => s.GeneratedAt).HasColumnName("AISummaryGeneratedAt");
        });
    }
}
