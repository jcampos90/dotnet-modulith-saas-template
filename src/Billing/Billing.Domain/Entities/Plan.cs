// src/Billing/Billing.Domain/Entities/Plan.cs
using MySaaS.BuildingBlocks;

namespace Billing.Domain.Entities;

public class Plan : Entity<Guid>
{
    public Guid UserId { get; private set; }    // Reference to Identity — plain Guid, no FK
    public Guid PlanId { get; private set; }    // FK to Plan (same module — this is fine)
    public string Status { get; private set; } = string.Empty;
    public DateTime CurrentPeriodEnd { get; private set; }

    public static Plan Create(Guid userId, Guid planId, DateTime periodEnd)
    {
        return new Plan
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            Status = "Trialing",
            CurrentPeriodEnd = periodEnd
        };
    }
}