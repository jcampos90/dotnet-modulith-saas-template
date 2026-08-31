using MediatR;

namespace Billing.Application.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid UserId) : IRequest<SubscriptionResult?>;

public sealed record SubscriptionResult(
    Guid Id, Guid PlanId, string Status, DateTime CurrentPeriodEnd);