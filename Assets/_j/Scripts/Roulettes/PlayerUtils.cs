namespace Roulettes
{
    // TODO _j Andrey not implemented ExperienceGain, CameraZoom
    public enum PlayerKind
    {
        Unknown,
        MeleeDamage,
        RangeDamage,
        AttackRange,
        AttackAccuracy,
        AttackRate,
        // ExperienceGain,
        Health,
        MovementSpeed,
        // CameraZoom,
    }

    public enum PlayerModifier
    {
        Unchanged,
        Increased,
        Decreased,
    }
}
