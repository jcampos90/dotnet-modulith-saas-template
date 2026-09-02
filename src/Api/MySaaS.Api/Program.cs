using Billing.Infrastructure;
using Features.Infrastructure;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Register module services — the composition root
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);
builder.Services.AddFeaturesModule(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

var app = builder.Build();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapControllers();
app.Run();