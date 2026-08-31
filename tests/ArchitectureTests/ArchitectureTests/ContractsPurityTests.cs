using NetArchTest.Rules;

namespace ArchitectureTests;

public class ContractsPurityTests
{
    [Fact]
    public void Contracts_Should_Not_Depend_On_EntityFrameworkCore()
    {
        var contractsAssemblies = new[]
        {
            typeof(Identity.Contracts.IIdentityService).Assembly,
            typeof(Billing.Contracts.SubscriptionCreatedEvent).Assembly,
            //typeof(Features.Contracts.FeatureFlags).Assembly
        };

        foreach (var assembly in contractsAssemblies)
        {
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("EntityFrameworkCore")
                .GetResult();

            Assert.True(result.IsSuccessful,
                $"{assembly.GetName().Name} contracts should not depend on EF Core");
        }
    }

    [Fact]
    public void Contracts_Should_Not_Depend_On_Domain()
    {
        var contractsAssemblies = new[]
        {
            typeof(Identity.Contracts.IIdentityService).Assembly,
            typeof(Billing.Contracts.SubscriptionCreatedEvent).Assembly,
            //typeof(Features.Contracts.FeatureFlags).Assembly
        };

        foreach (var assembly in contractsAssemblies)
        {
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("Domain")
                .GetResult();

            Assert.True(result.IsSuccessful,
                $"{assembly.GetName().Name} contracts should not depend on Domain");
        }
    }
}