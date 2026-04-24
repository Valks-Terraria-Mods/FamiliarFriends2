using System;
using Microsoft.Xna.Framework;

namespace FamiliarFriends;

public readonly record struct SliderRenderStyle(
    Color TrackBorderColor,
    Color TrackBackgroundColor,
    Color FillColor,
    Color KnobBorderColor,
    Color KnobFaceColor,
    Color KnobGripColor,
    Func<float, Color> GradientColorAt);
