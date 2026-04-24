using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class CharacterCreatorHairStylesSection : UIElement
{
    public CharacterCreatorHairStylesSection(bool showHeader = true)
    {
        Width.Set(0f, 1f);
        Height.Set(0f, 1f);
        SetPadding(0f);

        float contentTop = 0f;

        if (showHeader)
        {
            UIText title = new("Hair Styles")
            {
                HAlign = 0.5f,
                Top = StyleDimension.FromPixels(2f),
            };
            Append(title);
            contentTop = 24f;
        }

        UIElement listHost = new()
        {
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixelsAndPercent(-contentTop, 1f),
            Top = StyleDimension.FromPixels(contentTop),
        };
        listHost.SetPadding(0f);
        Append(listHost);

        UIScrollbar scrollbar = new()
        {
            HAlign = 1f,
            Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
        };
        listHost.Append(scrollbar);

        float scrollbarHalfWidth = scrollbar.Width.Pixels * 0.5f;
        scrollbar.Left.Set(scrollbarHalfWidth, 0f);

        UIGrid grid = [];
        grid.Width.Set(-scrollbarHalfWidth, 1f);
        grid.Height.Set(0f, 1f);
        grid.SetScrollbar(scrollbar);
        grid.ManualSortMethod = _ => { };
        listHost.Append(grid);

        for (int hairstyleId = 0; hairstyleId < TextureAssets.PlayerHair.Length; hairstyleId++)
            grid.Add(new PlayerHeadButton(hairstyleId));
    }
}
