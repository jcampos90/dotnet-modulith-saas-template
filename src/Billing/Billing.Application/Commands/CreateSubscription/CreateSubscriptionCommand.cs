using MediatR;

namespace Billing.Application.Commands.CreateSubscription;

public record CreateSubscriptionCommand(Guid UserId, Guid PlanId) : IRequest<Result>;

public record Result(Guid? SubscriptionId, bool IsSuccess, string? Error);