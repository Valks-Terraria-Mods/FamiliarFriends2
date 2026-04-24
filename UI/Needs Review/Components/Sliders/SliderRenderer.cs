using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace FamiliarFriends;

public static class SliderRenderer
{
    public static bool TryCreateGeometry(SliderGeometryBuildRequest request, out SliderGeometry geometry)
    {
        CalculatedStyle dimensions = request.Track.GetDimensions();
        geometry = default;

        if (dimensions.Width <= 2f || dimensions.Height <= 2f)
            return false;

        Rectangle outerRect = new((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height);
        Rectangle innerRect = new(outerRect.X + 1, outerRect.Y + 1, Math.Max(1, outerRect.Width - 2), Math.Max(1, outerRect.Height - 2));

        float normalizedValue = request.Range <= 0f ? 0f : (request.Value - request.MinValue) / request.Range;
        normalizedValue = MathHelper.Clamp(normalizedValue, 0f, 1f);

        int knobPixelWidth = Math.Max(1, (int)MathF.Round(request.KnobWidthBase));
        int knobPixelHeight = Math.Max(1, (int)MathF.Round(request.KnobHeightBase));
        int travelWidth = Math.Max(0, innerRect.Width - knobPixelWidth);

        int knobLeft = innerRect.X + (int)MathF.Round(travelWidth * normalizedValue);
        int knobTop = innerRect.Y + ((innerRect.Height - knobPixelHeight) / 2);
        int fillWidth = Math.Clamp(knobLeft + knobPixelWidth - innerRect.X, 0, innerRect.Width);

        Rectangle fillRect = new(innerRect.X, innerRect.Y, fillWidth, innerRect.Height);
        Rectangle knobRect = new(knobLeft, knobTop, knobPixelWidth, knobPixelHeight);
        Rectangle knobInnerRect = new(knobRect.X + 1, knobRect.Y + 1, Math.Max(1, knobRect.Width - 2), Math.Max(1, knobRect.Height - 2));

        geometry = new SliderGeometry(outerRect, innerRect, fillRect, knobRect, knobInnerRect);
        return true;
    }

    public static void Draw(
        SpriteBatch spriteBatch,
        SliderGeometry geometry,
        SliderRenderStyle style)
    {
        Texture2D pixel = TextureAssets.MagicPixel.Value;
        DrawTrack(spriteBatch, pixel, geometry, style.TrackBorderColor);

        if (style.GradientColorAt is null)
            DrawSolidTrackFill(spriteBatch, pixel, geometry, style);
        else
            DrawGradientFill(spriteBatch, pixel, geometry.InnerRect, style.GradientColorAt);

        DrawKnob(spriteBatch, pixel, geometry, style);
    }

    private static void DrawTrack(SpriteBatch spriteBatch, Texture2D pixel, SliderGeometry geometry, Color trackBorderColor)
    {
        spriteBatch.Draw(pixel, geometry.OuterRect, trackBorderColor);
    }

    private static void DrawSolidTrackFill(
        SpriteBatch spriteBatch,
        Texture2D pixel,
        SliderGeometry geometry,
        SliderRenderStyle style)
    {
        spriteBatch.Draw(pixel, geometry.InnerRect, style.TrackBackgroundColor);
        spriteBatch.Draw(pixel, geometry.FillRect, style.FillColor);
    }

    private static void DrawGradientFill(SpriteBatch spriteBatch, Texture2D pixel, Rectangle innerRect, Func<float, Color> gradientColorAt)
    {
        for (int x = 0; x < innerRect.Width; x++)
        {
            float point = innerRect.Width <= 1 ? 0f : x / (float)(innerRect.Width - 1);
            Color sampleColor = gradientColorAt(point);
            spriteBatch.Draw(pixel, new Rectangle(innerRect.X + x, innerRect.Y, 1, innerRect.Height), sampleColor);
        }
    }

    private static void DrawKnob(
        SpriteBatch spriteBatch,
        Texture2D pixel,
        SliderGeometry geometry,
        SliderRenderStyle style)
    {
        spriteBatch.Draw(pixel, geometry.KnobRect, style.KnobBorderColor);
        spriteBatch.Draw(pixel, geometry.KnobInnerRect, style.KnobFaceColor);

        int gripHeight = Math.Max(Math.Max(2, (int)MathF.Round(4f)), geometry.KnobInnerRect.Height - Math.Max(2, (int)MathF.Round(6f)));
        int gripTop = geometry.KnobInnerRect.Y + ((geometry.KnobInnerRect.Height - gripHeight) / 2);
        int gripInset = Math.Max(1, (int)MathF.Round(3f));
        int gripSpacing = Math.Max(1, (int)MathF.Round(2f));

        for (int i = 0; i < 3; i++)
        {
            int gripX = geometry.KnobInnerRect.X + gripInset + (i * gripSpacing);
            spriteBatch.Draw(pixel, new Rectangle(gripX, gripTop, 1, gripHeight), style.KnobGripColor);
        }
    }
}

public readonly record struct SliderGeometry(
    Rectangle OuterRect,
    Rectangle InnerRect,
    Rectangle FillRect,
    Rectangle KnobRect,
    Rectangle KnobInnerRect);
