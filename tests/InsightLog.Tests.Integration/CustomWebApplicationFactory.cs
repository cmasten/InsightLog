using InsightLog.API;
using InsightLog.Domain.Events;
using InsightLog.Infrastructure.Persistence;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsightLog.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<InsightLogDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Create shared in-memory connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<InsightLogDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Ensure DB is created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InsightLogDbContext>();
            db.Database.EnsureCreated(); // Required to initialize schema

            // Register a fake handler for the domain event
            services.AddScoped<INotificationHandler<JournalEntryCreatedDomainEvent>, FakeJournalEntryCreatedHandler>();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
    }
}
