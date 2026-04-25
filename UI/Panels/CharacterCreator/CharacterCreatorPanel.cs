using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class CharacterTab(UIColoredImageButton btn)
{
    public UIColoredImageButton Button { get; } = btn;
    public void SetColor(Color color) => Button.SetColor(color);
}

public class CharacterCreatorPanel : UIState, IHasMainElement, IClosable
{
    public UIElement MainElement { get; private set; }

    public event Action CloseRequested;

    private static Color BackgroundColor = new(29, 37, 69, 224);
    private static Color BorderColor = new(77, 96, 151);

    private readonly Dictionary<CharacterCreatorTab, CharacterTab> _tabs = [];

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

        _tabs.Add(CharacterCreatorTab.Appearance, new CharacterTab(GameAssets.Tabs.Appearance));
        _tabs.Add(CharacterCreatorTab.Clothing, new CharacterTab(GameAssets.Tabs.GetGenderIcon(Gender.Female)));
        _tabs.Add(CharacterCreatorTab.HairStyles, new CharacterTab(GameAssets.Tabs.HairStyles));
        _tabs.Add(CharacterCreatorTab.HairColor, new CharacterTab(GameAssets.Tabs.HairColor));
        _tabs.Add(CharacterCreatorTab.EyeColor, new CharacterTab(GameAssets.Tabs.EyeColor));
        _tabs.Add(CharacterCreatorTab.SkinColor, new CharacterTab(GameAssets.Tabs.SkinColor));
        _tabs.Add(CharacterCreatorTab.ShirtColor, new CharacterTab(GameAssets.Tabs.ShirtColor));
        _tabs.Add(CharacterCreatorTab.UnderShirtColor, new CharacterTab(GameAssets.Tabs.UnderShirtColor));
        _tabs.Add(CharacterCreatorTab.PantsColor, new CharacterTab(GameAssets.Tabs.PantsColor));
        _tabs.Add(CharacterCreatorTab.ShoesColor, new CharacterTab(GameAssets.Tabs.ShoesColor));

        foreach (var tab in _tabs.Values)
            grid.Add(tab.Button);

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
