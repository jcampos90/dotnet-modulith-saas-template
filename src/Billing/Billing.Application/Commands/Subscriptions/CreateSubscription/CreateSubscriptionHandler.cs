using Billing.Application.Outbox;
using Billing.Application.Repositories;
using Billing.Contracts;
using Billing.Domain.Entities;
using Identity.Contracts;
using MediatR;

namespace Billing.Application.Commands.Subscriptions.CreateSubscription;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, Result>
{
    private readonly ISubscriptionRepository _repository;
    private readonly IOutboxPublisher _outbox;
    private readonly IIdentityService _identityService;

    public CreateSubscriptionHandler(ISubscriptionRepository repository, IOutboxPublisher outbox, IIdentityService identityService)
    {
        _repository = repository;
        _outbox = outbox;
        _identityService = identityService;
    }

    public async Task<Result> Handle(CreateSubscriptionCommand command, CancellationToken ct)
    {
        var user = await _identityService.GetUserByIdAsync(command.UserId);
        if (user is null)
            return new Result(null, false, "User does not exist");

        var subscription = Subscription.Create(command.UserId, command.PlanId, DateTime.UtcNow.AddMonths(1));

        await _repository.AddAsync(subscription);

        // Write event to outbox — same transaction as the subscription save
        await _outbox.AddAsync(new SubscriptionCreatedEvent(
            subscription.Id,
            subscription.UserId,
            subscription.PlanId,
            subscription.CurrentPeriodEnd), ct);

        await _repository.SaveChangesAsync(ct); 

        return new Result(subscription.Id, true, null);
    }
}
