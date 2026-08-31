using MySaaS.BuildingBlocks;

namespace Identity.Domain.Entities;

public class Role : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class UserRole: Entity<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}