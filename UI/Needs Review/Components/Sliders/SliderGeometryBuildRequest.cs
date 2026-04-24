using Terraria.UI;

namespace FamiliarFriends;

public readonly record struct SliderGeometryBuildRequest(
    UIElement Track,
    float Value,
    float MinValue,
    float Range,
    float KnobWidthBase,
    float KnobHeightBase);
