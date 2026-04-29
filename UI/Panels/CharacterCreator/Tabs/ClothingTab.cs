using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class ClothingTab(UIElement parent, Player player) : CharacterTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.GetGenderIcon(Gender.Female);
    protected override string Title { get; } = "Clothing Style";

    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        const int GridWidth = 240;

        const float VertGridTitleInset = 0.12f;
        const float HorzGridTitleInset = 0.25f;
        const float HorzGridInset = 0.05f;
        const float VertGridInset = 0.3f;
        const float GenderBtnSeparation = 0.045f;
        const float GenderBtnInset = 0.65f;

        var maleTitle = new UIText("Male")
        {
            HAlign = HorzGridTitleInset,
            VAlign = VertGridTitleInset
        };

        var femaleTitle = new UIText("Female")
        {
            HAlign = 1.0f - HorzGridTitleInset,
            VAlign = VertGridTitleInset
        };

        var maleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(GridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = HorzGridInset,
            VAlign = VertGridInset,
        };

        var femaleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(GridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = 1.0f - HorzGridInset,
            VAlign = VertGridInset,
        };

        var maleStyles = StaticClothStyleButton.MaleStyles;
        var femaleStyles = StaticClothStyleButton.FemaleStyles;

        for (int i = 0; i < maleStyles.Length; i++)
            maleGrid.Add(new StaticClothStyleButton(Player, maleStyles[i]));

        for (int i = 0; i < femaleStyles.Length; i++)
            femaleGrid.Add(new StaticClothStyleButton(Player, femaleStyles[i]));

        var maleGenderBtn = GameAssets.Tabs.GetGenderIcon(Gender.Male);
        var femaleGenderBtn = GameAssets.Tabs.GetGenderIcon(Gender.Female);

        maleGenderBtn.HAlign = 0.5f - GenderBtnSeparation;
        maleGenderBtn.VAlign = GenderBtnInset;

        femaleGenderBtn.HAlign = 0.5f + GenderBtnSeparation;
        femaleGenderBtn.VAlign = GenderBtnInset;

        if (Player.Male)
            maleGenderBtn.SetSelected(true);
        else
            femaleGenderBtn.SetSelected(true);

        maleGenderBtn.OnLeftClick += (_, _) =>
        {
            maleGenderBtn.SetSelected(true);
            femaleGenderBtn.SetSelected(false);  
        };

        femaleGenderBtn.OnLeftClick += (_, _) =>
        {
            maleGenderBtn.SetSelected(false);
            femaleGenderBtn.SetSelected(true);  
        };

        content.Append(maleTitle);
        content.Append(maleGrid);
        content.Append(femaleTitle);
        content.Append(femaleGrid);
        content.Append(maleGenderBtn);
        content.Append(femaleGenderBtn);
    }
}
