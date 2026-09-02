using System.Text;
using Billing.Infrastructure;
using Features.Infrastructure;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using MySaaS.Api.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register module services — the composition root
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);
builder.Services.AddFeaturesModule(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

// OpenAPI document generation
builder.Services.AddOpenApi("v1");

// --- Authentication ---
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("role", "Admin"));

    //options.AddPolicy("SameTenant", policy =>
    //    policy.Requirements.Add(new SameTenantRequirement()));

    options.AddPolicy("Subscriber", policy =>
        policy.RequireClaim("role", "Admin", "Subscriber"));
});

var app = builder.Build();

// --- Auth middleware (must be before MapControllers) ---
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

// Serve the OpenAPI spec and UI in development only
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");
    app.MapScalarApiReference();
}

// Correlation ID — runs before everything else
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();
app.Run();