using Identity.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly IdentityDbContext _dbContext;

    public IdentityService(IdentityDbContext dbContext) => _dbContext = dbContext;

    public async Task<UserResult?> GetUserByIdAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        return user is null ? null : new UserResult(user.Id, user.Email, user.TenantId);
    }

    public async Task<bool> UserExistsAsync(Guid userId) =>
        await _dbContext.Users.AnyAsync(u => u.Id == userId);

    public async Task<IReadOnlyList<UserResult>> GetUsersByIdsAsync(IEnumerable<Guid> userIds)
    {
        return await _dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new UserResult(u.Id, u.Email, u.TenantId))
            .ToListAsync();
    }
}