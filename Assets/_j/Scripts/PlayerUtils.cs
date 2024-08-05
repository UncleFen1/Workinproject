// TODO _j move to namespace Roulettes
namespace PlayerUtils
{
    public enum PlayerKind
    {
        Unknown,
        MeleeDamage,
        RangeDamage,
        AttackRange,
        AttackAccuracy,
        AttackRate,
        ExperienceGain,
        Health,
        MovementSpeed,
        CameraZoom,
    }

    public enum PlayerModifier
    {
        Unchanged,
        Increased,
        Decreased,
    }
}
