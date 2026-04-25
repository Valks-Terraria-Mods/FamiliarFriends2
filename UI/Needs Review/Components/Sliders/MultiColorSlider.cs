using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public abstract class MultiColorSlider : UIElement
{
    public event Action<Color> ColorChanged;

    public Color SelectedColor { get; private set; }

    protected void PublishSelectedColor(Color color, bool notify)
    {
        SelectedColor = color;

        if (notify)
            ColorChanged?.Invoke(color);
    }

    protected static bool TryParseHexColor(string hexText, out Color color)
    {
        if (string.IsNullOrWhiteSpace(hexText))
        {
            color = Color.White;
            return false;
        }

        string normalized = hexText.Trim().TrimStart('#');

        if (normalized.Length != 6)
        {
            color = Color.White;
            return false;
        }

        if (!uint.TryParse(normalized, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint packed))
        {
            color = Color.White;
            return false;
        }

        int red = (int)((packed >> 16) & 0xFF);
        int green = (int)((packed >> 8) & 0xFF);
        int blue = (int)(packed & 0xFF);
        color = new Color(red, green, blue);

        return true;
    }

    protected static UIColoredImageButton SetupActionButton(UIColoredImageButton button, float leftPixels, Action action)
    {
        button.Left = StyleDimension.FromPixels(leftPixels);
        button.SetVisibility(1f, 0.55f);
        button.OnLeftMouseDown += (_, _) => action();
        return button;
    }
}
