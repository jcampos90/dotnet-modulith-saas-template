using NetArchTest.Rules;
using System.Reflection;

namespace ArchitectureTests;

public class ModuleBoundaryTests
{
    [Fact]
    public void Identity_Should_Not_Depend_On_Billing()
    {
        var result = Types
            .InAssembly(typeof(Identity.Domain.Entities.User).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Billing")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Identity module must not reference Billing module");
    }

    [Fact]
    public void Identity_Should_Not_Depend_On_Features()
    {
        var result = Types
            .InAssembly(typeof(Identity.Domain.Entities.User).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Features")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Identity module must not reference Features module");
    }

    [Fact]
    public void Billing_Should_Not_Depend_On_Identity()
    {
        var result = Types
            .InAssembly(typeof(Billing.Domain.Entities.Subscription).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Identity")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Billing module must not reference Identity module");
    }

    [Fact]
    public void Billing_Should_Not_Depend_On_Features()
    {
        var result = Types
            .InAssembly(typeof(Billing.Domain.Entities.Subscription).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Features")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Billing module must not reference Features module");
    }

    [Fact]
    public void Features_Should_Not_Depend_On_Billing()
    {
        var result = Types
            .InAssembly(typeof(Features.Domain.Entities.Entitlement).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Billing")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Features module must not reference Billing module");
    }

    [Fact]
    public void Features_Should_Not_Depend_On_Identity()
    {
        var result = Types
            .InAssembly(typeof(Features.Domain.Entities.Entitlement).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Identity")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Features module must not reference Identity module");
    }
}