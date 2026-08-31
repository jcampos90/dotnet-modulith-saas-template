
using MySaaS.BuildingBlocks;

namespace Features.Domain.Entities;

public class Rollout : Entity<Guid>
{
    public Guid FeatureFlagId { get; private set; }    // FK to FeatureFlag
    public string Target { get; private set; } = string.Empty;
    public string Variation { get; private set; } = string.Empty;

    public static Rollout Create(Guid featureFlagId, string target, string variation)
    {
        return new Rollout
        {
            Id = Guid.NewGuid(),
            FeatureFlagId = featureFlagId,
            Target = target,
            Variation = variation
        };
    }
}