using Billing.Domain.Entities;

namespace Billing.Application.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<Subscription?> GetByUserIdAsync(Guid id);
    Task AddAsync(Subscription subscription);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
