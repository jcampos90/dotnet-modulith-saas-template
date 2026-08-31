using Features.Domain.Entities;

namespace Features.Application.Repositories;

public interface IEntitlementRepository
{
    Task<Entitlement?> GetByPlanIdAsync(Guid id);
    Task<List<string>> GetPlanFeatureKeysByPlanIdAsync(Guid planId);
    Task AddAsync(Entitlement entitlement);
    Task AddRangeAsync(IEnumerable<Entitlement> entitlements);

    Task SaveChangesAsync(CancellationToken ct = default);
}
