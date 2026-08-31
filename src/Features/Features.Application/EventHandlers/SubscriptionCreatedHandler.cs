using Billing.Contracts;
using Features.Application.Repositories;
using Features.Domain.Entities;
using MediatR;

namespace Features.Application.EventHandlers;

internal sealed class SubscriptionCreatedHandler : INotificationHandler<SubscriptionCreatedEvent>
{
    private readonly IEntitlementRepository _entitlementRepository;

    public SubscriptionCreatedHandler(IEntitlementRepository entitlementRepository)
    {
        _entitlementRepository = entitlementRepository;
    }

    public async Task Handle(SubscriptionCreatedEvent notification, CancellationToken ct)
    {
        // Look up which features this plan enables
        var planFeatures = await _entitlementRepository.GetPlanFeatureKeysByPlanIdAsync(notification.PlanId);

        // Create entitlements for the new subscriber
        var entitlements = planFeatures.Select(featureKey =>
            Entitlement.Create(notification.UserId, notification.PlanId, featureKey, true));

        await _entitlementRepository.AddRangeAsync(entitlements);

        await _entitlementRepository.SaveChangesAsync(ct);
    }
}