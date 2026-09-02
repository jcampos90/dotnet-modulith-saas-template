using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Features.Infrastructure;

public static class FeaturesModuleExtensions
{
    public static IServiceCollection AddFeaturesModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FeaturesDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "features")));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(FeaturesModuleExtensions).Assembly));

        // debugger is attached
        if(System.Diagnostics.Debugger.IsAttached)
        {
            // run migrations on startup in development environment
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FeaturesDbContext>();
            dbContext.Database.Migrate();
        }

        return services;
    }
}