
using MySaaS.BuildingBlocks;

namespace Billing.Domain.Entities;

public class Invoice : Entity<Guid>
{
    public Guid UserId { get; private set; }    // Reference to Identity — plain Guid, no FK
    public Guid PlanId { get; private set; }    // FK to Plan (same module — this is fine)
    public string Status { get; private set; } = string.Empty;
    public DateTime CurrentPeriodEnd { get; private set; }

    public static Invoice Create(Guid userId, Guid planId, DateTime periodEnd)
    {
        return new Invoice
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            Status = "Pending",
            CurrentPeriodEnd = periodEnd
        };
    }
}