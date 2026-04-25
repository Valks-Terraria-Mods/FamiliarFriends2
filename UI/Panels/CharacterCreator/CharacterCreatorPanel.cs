using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class CharacterCreatorPanel : UIState, IHasMainElement, IClosable
{
    public UIElement MainElement { get; private set; }

    public event Action CloseRequested;

    private static Color BackgroundColor = new(29, 37, 69, 224);
    private static Color BorderColor = new(77, 96, 151);

    public override void OnInitialize()
    {
        base.OnInitialize();
        var panel = new UIPanel();
        panel.Width.Set(600, 0);
        panel.Height.Set(350, 0);
        panel.BackgroundColor = BackgroundColor;
        panel.BorderColor = BorderColor;
        panel.HAlign = 0.5f;
        panel.VAlign = 0.5f;
        Append(panel);

        // TABS
        var grid = new UIGrid()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPixels(200)
        };

        grid.Add(GameAssets.Tabs.Appearance);
        grid.Add(GameAssets.Tabs.GetGenderIcon(Gender.Female));
        grid.Add(GameAssets.Tabs.HairStyles);
        grid.Add(GameAssets.Tabs.HairColor);
        grid.Add(GameAssets.Tabs.EyeColor);
        grid.Add(GameAssets.Tabs.SkinColor);
        grid.Add(GameAssets.Tabs.ShirtColor);
        grid.Add(GameAssets.Tabs.UnderShirtColor);
        grid.Add(GameAssets.Tabs.PantsColor);
        grid.Add(GameAssets.Tabs.ShoesColor);

        // NAME
        var inputField = new InputField("", "");
        grid.Add(inputField);

        panel.Append(grid);

        // CLOTHING
        var maleStyles = StaticClothStyleButton.MaleStyles;
        var femaleStyles = StaticClothStyleButton.FemaleStyles;

        const int gridWidth = 240;

        var maleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(gridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = 0f,
            VAlign = 1f
        };

        var femaleGrid = new UIGrid()
        {
            Width = StyleDimension.FromPixels(gridWidth),
            Height = StaticClothStyleButton.Height,
            HAlign = 1f,
            VAlign = 1f
        };

        for (int i = 0; i < maleStyles.Length; i++)
            maleGrid.Add(new StaticClothStyleButton(maleStyles[i]));

        for (int i = 0; i < femaleStyles.Length; i++)
            femaleGrid.Add(new StaticClothStyleButton(femaleStyles[i]));

        panel.Append(maleGrid);
        panel.Append(femaleGrid);

        // CLOSE BUTTON
        var closeButton = UiControlFactory.CloseBtn();
        closeButton.OnLeftClick += (_, _) => CloseRequested?.Invoke();
        panel.Append(closeButton);
        
        MainElement = panel;
    }
}
