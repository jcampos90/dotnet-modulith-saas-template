using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Billing.Infrastructure;
using Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<MySaaS.Api.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Clear();
            config.AddJsonFile("appsettings.json");
        });

        builder.ConfigureTestServices(services =>
        {
            // Remove ALL EF Core and Npgsql related services
            var toRemove = services.Where(d =>
                d.ServiceType.FullName?.Contains("DbContext") == true ||
                d.ServiceType.FullName?.Contains("EntityFrameworkCore") == true ||
                d.ImplementationType?.FullName?.Contains("EntityFrameworkCore") == true ||
                d.ImplementationType?.FullName?.Contains("Npgsql") == true
            ).ToList();

            foreach (var descriptor in toRemove)
                services.Remove(descriptor);

            // Add InMemory providers
            services.AddDbContext<BillingDbContext>(options =>
                options.UseInMemoryDatabase("BillingTestDb"));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityTestDb"));
        });
    }
}
