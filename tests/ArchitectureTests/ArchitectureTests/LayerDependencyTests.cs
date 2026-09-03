using NetArchTest.Rules;

namespace ArchitectureTests;

public class LayerDependencyTests
{
    [Fact]
    public void Domain_Layers_Should_Not_Depend_On_Application()
    {
        var domainAssemblies = new[]
        {
            typeof(Identity.Domain.Entities.User).Assembly,
            typeof(Billing.Domain.Entities.Subscription).Assembly,
            typeof(Features.Domain.Entities.Entitlement).Assembly
        };

        foreach (var assembly in domainAssemblies)
        {
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("Application")
                .GetResult();

            Assert.True(result.IsSuccessful,
                $"{assembly.GetName().Name} domain should not depend on Application");
        }
    }

    [Fact]
    public void Domain_Layers_Should_Not_Depend_On_Infrastructure()
    {
        var domainAssemblies = new[]
        {
            typeof(Identity.Domain.Entities.User).Assembly,
            typeof(Billing.Domain.Entities.Subscription).Assembly,
            typeof(Features.Domain.Entities.Entitlement).Assembly
        };

        foreach (var assembly in domainAssemblies)
        {
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful,
                $"{assembly.GetName().Name} domain should not depend on Infrastructure");
        }
    }

    [Fact]
    public void Application_Layers_Should_Not_Depend_On_Infrastructure()
    {
        // TODO: Add more application assemblies as they are created
        var appAssemblies = new[]
        {
            //typeof(Identity.Application.Commands.CreateUser.CreateUserHandler).Assembly,
            typeof(Billing.Application.Commands.Subscriptions.CreateSubscription.CreateSubscriptionHandler).Assembly
        };

        foreach (var assembly in appAssemblies)
        {
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful,
                $"{assembly.GetName().Name} application should not depend on Infrastructure");
        }
    }
}