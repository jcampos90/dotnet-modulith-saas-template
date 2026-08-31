using Microsoft.Extensions.DependencyInjection;

namespace MySaaS.BuildingBlocks;

/// <summary>
/// Each module implements this to register its services.
/// </summary>
public interface IModule
{
    void RegisterServices(IServiceCollection services);
}