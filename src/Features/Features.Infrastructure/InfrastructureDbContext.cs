// src/Features/Features.Infrastructure/FeaturesDbContext.cs
using Features.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Features.Infrastructure;

public class FeaturesDbContext : DbContext
{
    public FeaturesDbContext(DbContextOptions<FeaturesDbContext> options)
        : base(options) { }

    public DbSet<FeatureFlag> FeatureFlags => Set<FeatureFlag>();
    public DbSet<Entitlement> Entitlements => Set<Entitlement>();
    public DbSet<Rollout> Rollouts => Set<Rollout>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("features");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FeaturesDbContext).Assembly);
    }
}