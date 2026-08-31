// src/Identity/Identity.PublicApi/Program.cs
using Identity.Infrastructure;
using Features.Infrastructure;
using Billing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



// Register module services
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddFeaturesModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.Run();