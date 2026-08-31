namespace Identity.Contracts;

public interface IIdentityService
{
    Task<UserResult?> GetUserByIdAsync(Guid userId);
    Task<bool> UserExistsAsync(Guid userId);
    Task<IReadOnlyList<UserResult>> GetUsersByIdsAsync(IEnumerable<Guid> userIds);
}

public sealed record UserResult(Guid UserId, string Email, string TenantId);