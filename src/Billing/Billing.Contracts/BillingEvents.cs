using MediatR;

namespace Billing.Contracts;

public sealed record SubscriptionCreatedEvent(
    Guid SubscriptionId,
    Guid UserId,
    Guid PlanId,
    DateTime CurrentPeriodEnd) : INotification;
