using MySaaS.BuildingBlocks;

namespace Identity.Domain.Entities;

public class User : Entity<Guid>
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string TenantId { get; private set; } = string.Empty;

    // No reference to Billing. No subscription_id. No plan_id.
    // Identity's job is auth and user management. That's it.

    public static User Create(string email, string passwordHash, string tenantId)
    {
        return new User
        {
            Id = Guid.CreateVersion7(),
            Email = email,
            PasswordHash = passwordHash,
            TenantId = tenantId
        };
    }
}