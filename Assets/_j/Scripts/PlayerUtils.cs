// TODO _j move to namespace Roulettes
namespace PlayerUtils
{
    // TODO _j Andrey not implemented MeleeDamage, ExperienceGain, CameraZoom
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
