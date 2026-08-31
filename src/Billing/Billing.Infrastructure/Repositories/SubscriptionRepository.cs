using Billing.Application.Repositories;
using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Repositories;

internal sealed class SubscriptionRepository : ISubscriptionRepository
{
    private readonly BillingDbContext _dbContext;

    public SubscriptionRepository(BillingDbContext dbContext) => _dbContext = dbContext;

    public async Task<Subscription?> GetByIdAsync(Guid id) =>
        await _dbContext.Subscriptions.FindAsync(id);

    public async Task<Subscription?> GetByUserIdAsync(Guid userId) =>
        await _dbContext.Subscriptions.FirstOrDefaultAsync(s => s.UserId == userId);

    public async Task AddAsync(Subscription subscription) =>
        await _dbContext.Subscriptions.AddAsync(subscription);
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}
