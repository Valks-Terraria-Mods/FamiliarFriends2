using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace FamiliarFriends;

public class FriendAiConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(100)]
    [Increment(1)]
    [Range(0, 200)]
    public float MinFollowDistance { get; set; } = 100;

    [DefaultValue(0.1f)]
    [Increment(0.01f)]
    [Range(0, 0.2f)]
    public float MinWalkSpeed { get; set; } = 0.1f;

    [DefaultValue(3)]
    [Increment(0.01f)]
    [Range(0, 5)]
    public float MaxWalkSpeed { get; set; } = 3;

    [DefaultValue(0.03f)]
    [Increment(0.01f)]
    [Range(0, 0.3f)]
    public float WalkAcceleration { get; set; } = 0.03f;

    [DefaultValue(0.1f)]
    [Increment(0.01f)]
    [Range(0, 0.3f)]
    public float WalkDeacceleration { get; set; } = 0.1f;
}
