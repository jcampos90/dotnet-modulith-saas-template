using System.Net;
using System.Net.Http.Json;
using Identity.Domain.Entities;
using Identity.Infrastructure;
using IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests.Billing;

public class CreateSubscriptionTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory _factory;

    public CreateSubscriptionTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    private async Task<Guid> SeedUserAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var identityDb = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

        var user = User.Create("test@example.com", "hashed_password", "tenant-1");
        identityDb.Users.Add(user);
        await identityDb.SaveChangesAsync();

        return user.Id;
    }

    [Fact]
    public async Task CreateSubscription_WithValidUser_Returns_Success()
    {
        var userId = await SeedUserAsync();
        var command = new { UserId = userId, PlanId = Guid.NewGuid() };

        var response = await _client.PostAsJsonAsync("/api/subscriptions", command);

        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}, Body: {body}");
    }

    [Fact]
    public async Task CreateSubscription_WithInvalidUser_Returns_BadRequest()
    {
        var command = new { UserId = Guid.NewGuid(), PlanId = Guid.NewGuid() };

        var response = await _client.PostAsJsonAsync("/api/subscriptions", command);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("does not exist", body);
    }
}
