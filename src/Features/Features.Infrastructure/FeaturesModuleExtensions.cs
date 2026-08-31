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
        return services;
    }
}