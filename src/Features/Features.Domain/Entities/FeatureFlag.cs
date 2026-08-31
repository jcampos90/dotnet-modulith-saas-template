
using MySaaS.BuildingBlocks;

namespace Features.Domain.Entities;

public class FeatureFlag : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public bool IsEnabled { get; private set; }

    public static FeatureFlag Create(string name, bool isEnabled)
    {
        return new FeatureFlag
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsEnabled = isEnabled
        };
    }
}