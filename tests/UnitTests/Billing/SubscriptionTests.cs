using Billing.Domain.Entities;

namespace UnitTests;

public class SubscriptionTests
{
    [Fact]
    public void Create_should_set_default_values()
    {

        var subscription = Subscription.Create(Guid.CreateVersion7(), Guid.CreateVersion7(), DateTime.UtcNow.AddMonths(1));

        Assert.NotEqual(Guid.Empty, subscription.Id);
        Assert.Equal("Trialing", subscription.Status);
    }

    [Fact]
    public void Create_should_reject_empty_user_id()
    {
        Assert.Throws<ArgumentException>(() =>
            Subscription.Create(Guid.Empty, Guid.CreateVersion7(), DateTime.UtcNow.AddMonths(1)));
    }
}
