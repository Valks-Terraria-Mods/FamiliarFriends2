using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class ClothingTab(UIElement parent) : CharacterTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.GetGenderIcon(Gender.Female);
    protected override string Title { get; } = "Clothing";

    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        var maleStyles = StaticClothStyleButton.MaleStyles;
        var femaleStyles = StaticClothStyleButton.FemaleStyles;

        const int GridWidth = 240;
        const float HorzInset = 0.05f;
        const float VertInset = 0.1f;

        var maleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(GridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = HorzInset,
            VAlign = 1.0f - VertInset,
        };

        var femaleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(GridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = 1.0f - HorzInset,
            VAlign = 1.0f - VertInset,
        };

        for (int i = 0; i < maleStyles.Length; i++)
            maleGrid.Add(new StaticClothStyleButton(maleStyles[i]));

        for (int i = 0; i < femaleStyles.Length; i++)
            femaleGrid.Add(new StaticClothStyleButton(femaleStyles[i]));

        content.Append(maleGrid);
        content.Append(femaleGrid);
    }
}
