// src/Billing/Billing.Domain/Entities/Subscription.cs
using MySaaS.BuildingBlocks;

namespace Billing.Domain.Entities;

public class Subscription : Entity<Guid>
{
    public Guid UserId { get; private set; }    // Reference to Identity — plain Guid, no FK
    public Guid PlanId { get; private set; }    // FK to Plan (same module — this is fine)
    public string Status { get; private set; } = string.Empty;
    public DateTime CurrentPeriodEnd { get; private set; }

    // UserId is a reference. We store it because we need to know
    // which user this subscription belongs to. But we don't enforce
    // referential integrity with Identity's users table.

    public static Subscription Create(Guid userId, Guid planId, DateTime periodEnd)
    {
        return new Subscription
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            Status = "Trialing",
            CurrentPeriodEnd = periodEnd
        };
    }
}