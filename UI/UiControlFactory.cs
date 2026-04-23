using Terraria.ModLoader.UI;
using Terraria.UI;

namespace FamiliarFriends;

public static class UiControlFactory
{
    /// <summary>
    /// Creates a close button docked to the top right.
    /// </summary>
    public static UIButton<string> CloseBtn()
    {
        const int size = 32;

        return new UIButton<string>("X")
        {
            Width = StyleDimension.FromPixels(size),
            Height = StyleDimension.FromPixels(size),
            HAlign = 1f,
        };
    }
}
