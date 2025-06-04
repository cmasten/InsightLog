using InsightLog.Application.Common;
using InsightLog.Application.Events.Handlers;
using InsightLog.Application.Interfaces;
using InsightLog.Infrastructure.Persistence;
using InsightLog.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsightLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InsightLogDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAISummaryGenerator, FakeAISummaryGenerator>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<JournalEntryCreatedHandler>());

        return services;
    }
}
