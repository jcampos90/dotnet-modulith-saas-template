using NetArchTest.Rules;

namespace ArchitectureTests;

public class NamingTests
{
    [Fact]
    public void Handlers_Should_Have_Handler_Suffix()
    {
        var result = Types
            .InAssembly(typeof(Billing.Application.Commands.Subscriptions.CreateSubscription.CreateSubscriptionHandler).Assembly)
            .That   ()
            .ImplementInterface(typeof(MediatR.IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "All IRequestHandler implementations should end with 'Handler'");
    }

    [Fact]
    public void Domain_Entities_Should_Reside_In_Entities_Namespace()
    {
        var result = Types
            .InAssembly(typeof(Billing.Domain.Entities.Subscription).Assembly)
            .That()
            .Inherit(typeof(MySaaS.BuildingBlocks.Entity<>))
            .Should()
            .ResideInNamespaceContaining("Entities")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Domain entities should reside in a namespace containing 'Entities'");
    }
}