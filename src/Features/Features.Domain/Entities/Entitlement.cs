using MySaaS.BuildingBlocks;

namespace Features.Domain.Entities;

public class Entitlement : Entity<Guid>
{
    public Guid UserId { get; private set; }      // Reference to Identity
    public Guid PlanId { get; private set; }      // Reference to Billing
    public string FeatureKey { get; private set; } = string.Empty;
    public bool Enabled { get; private set; }

    // Entitlements are derived from the plan. When Billing publishes
    // a SubscriptionUpgradedEvent, Features creates new Entitlement rows
    // for that user. No cross-context queries needed.

    public static Entitlement Create(Guid userId, Guid planId, string featureKey, bool enabled)
    {
        return new Entitlement
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            FeatureKey = featureKey,
            Enabled = enabled
        };
    }
}