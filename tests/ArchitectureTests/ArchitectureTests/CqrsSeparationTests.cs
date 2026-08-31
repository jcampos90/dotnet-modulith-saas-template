using NetArchTest.Rules;

namespace ArchitectureTests;

public class CqrsSeparationTests
{
    [Fact]
    public void Commands_Should_Not_Depend_On_Queries()
    {
        var result = Types
            .InAssembly(typeof(Billing.Application.Commands.CreateSubscription.CreateSubscriptionHandler).Assembly)
            .That()
            .ResideInNamespace("Billing.Application.Commands")
            .ShouldNot()
            .HaveDependencyOn("Billing.Application.Queries")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Commands should not depend on Queries");
    }

    [Fact]
    public void Queries_Should_Not_Depend_On_Commands()
    {
        var result = Types
            .InAssembly(typeof(Billing.Application.Queries.GetSubscription.GetSubscriptionHandler).Assembly)
            .That()
            .ResideInNamespace("Billing.Application.Queries")
            .ShouldNot()
            .HaveDependencyOn("Billing.Application.Commands")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Queries should not depend on Commands");
    }
}