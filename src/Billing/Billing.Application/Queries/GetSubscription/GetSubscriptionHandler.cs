using Billing.Application.Repositories;
using MediatR;
using System.Data;

namespace Billing.Application.Queries.GetSubscription;

public class GetSubscriptionHandler : IRequestHandler<GetSubscriptionQuery, SubscriptionResult?>
{
    private readonly ISubscriptionRepository _repository;


    public GetSubscriptionHandler(ISubscriptionRepository repository) => _repository = repository;

    public async Task<SubscriptionResult?> Handle(GetSubscriptionQuery query, CancellationToken ct)
    {
        
        var subscription = await _repository.GetByUserIdAsync(query.UserId);
        return subscription is null ? null : new SubscriptionResult(
            subscription.Id,
            subscription.PlanId,
            subscription.Status,
            subscription.CurrentPeriodEnd
        );
    }
}
