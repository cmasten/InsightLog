using InsightLog.Application.Features.JournalEntries;

namespace InsightLog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateJournalEntry).Assembly));

        services.AddValidatorsFromAssembly(typeof(CreateJournalEntry).Assembly);

        return services;
    }
}
