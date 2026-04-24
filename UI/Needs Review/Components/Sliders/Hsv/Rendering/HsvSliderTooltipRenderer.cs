using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;

namespace FamiliarFriends;

public static class HsvSliderTooltipRenderer
{
    public static void DrawIfHovering(SpriteBatch spriteBatch, bool copyHovered, bool pasteHovered, bool randomizeHovered)
    {
        if (!TryGetTooltip(copyHovered, pasteHovered, randomizeHovered, out string tooltip))
            return;

        Vector2 drawPos = CalculateDrawPosition(tooltip);
        byte textShade = Main.mouseTextColor;
        Color textColor = new(textShade, textShade, textShade);

        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, tooltip, drawPos.X, drawPos.Y, textColor, Color.Black, Vector2.Zero);
    }

    private static bool TryGetTooltip(bool copyHovered, bool pasteHovered, bool randomizeHovered, out string tooltip)
    {
        tooltip = null;

        if (copyHovered)
            tooltip = Language.GetTextValue("UI.CopyColorToClipboard");
        else if (pasteHovered)
            tooltip = Language.GetTextValue("UI.PasteColorFromClipboard");
        else if (randomizeHovered)
            tooltip = Language.GetTextValue("UI.RandomizeColor");

        return !string.IsNullOrEmpty(tooltip);
    }

    private static Vector2 CalculateDrawPosition(string tooltip)
    {
        float textWidth = FontAssets.MouseText.Value.MeasureString(tooltip).X;
        Vector2 drawPos = new(Main.mouseX + 16f, Main.mouseY + 16f);

        if (drawPos.Y > Main.screenHeight - 30)
            drawPos.Y = Main.screenHeight - 30;

        if (drawPos.X > Main.screenWidth - textWidth)
            drawPos.X = Main.screenWidth - textWidth - 16f;

        return drawPos;
    }
}