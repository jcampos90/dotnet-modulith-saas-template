using Billing.Application.Outbox;
using Billing.Application.Repositories;
using Billing.Infrastructure.Repositories;
using Billing.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Infrastructure;

public static class BillingModuleExtensions
{
    public static IServiceCollection AddBillingModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BillingDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "billing")));
        
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IOutboxPublisher, OutboxPublisher>();
        services.AddHostedService<OutboxProcessor>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(BillingModuleExtensions).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(Application.Commands.Subscriptions.CreateSubscription.CreateSubscriptionHandler).Assembly);
        });

        // debugger is attached
        if(System.Diagnostics.Debugger.IsAttached)
        {
            // run migrations on startup in development environment
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BillingDbContext>();
            dbContext.Database.Migrate();
        }

        return services;
    }
}
