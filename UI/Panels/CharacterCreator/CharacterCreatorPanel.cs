using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class CharacterTab
{
    public UIColoredImageButton Button { get; }
    public UIElement Content { get; }

    private readonly UIElement _mainContent;

    public CharacterTab(UIElement mainContent, UIColoredImageButton btn, UIElement content)
    {
        _mainContent = mainContent;
        Button = btn;
        Content = content;

        btn.OnLeftClick += (_, _) => Select();
    }
    
    public void SetColor(Color color) => Button.SetColor(color);
    public void Select()
    {
        _mainContent.RemoveAllChildren();
        _mainContent.Append(Content);
    }
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
            Height = StyleDimension.Fill
        };

        var mainContent = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPixels(275)
        };

        _tabs.Add(CharacterCreatorTab.Appearance, new CharacterTab(mainContent, GameAssets.Tabs.Appearance, BuildAppearanceTab()));
        _tabs.Add(CharacterCreatorTab.Clothing, new CharacterTab(mainContent, GameAssets.Tabs.GetGenderIcon(Gender.Female), BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.HairStyles, new CharacterTab(mainContent, GameAssets.Tabs.HairStyles, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.HairColor, new CharacterTab(mainContent, GameAssets.Tabs.HairColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.EyeColor, new CharacterTab(mainContent, GameAssets.Tabs.EyeColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.SkinColor, new CharacterTab(mainContent, GameAssets.Tabs.SkinColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.ShirtColor, new CharacterTab(mainContent, GameAssets.Tabs.ShirtColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.UnderShirtColor, new CharacterTab(mainContent, GameAssets.Tabs.UnderShirtColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.PantsColor, new CharacterTab(mainContent, GameAssets.Tabs.PantsColor, BuildColorTab()));
        _tabs.Add(CharacterCreatorTab.ShoesColor, new CharacterTab(mainContent, GameAssets.Tabs.ShoesColor, BuildColorTab()));

        foreach (var tab in _tabs.Values)
            grid.Add(tab.Button);

        grid.Add(mainContent);

        panel.Append(grid);

        _tabs[CharacterCreatorTab.Appearance].Select();

        // CLOSE BUTTON
        var closeButton = UiControlFactory.CloseBtn();
        closeButton.OnLeftClick += (_, _) => CloseRequested?.Invoke();
        panel.Append(closeButton);
        
        MainElement = panel;
    }

    private static UIElement BuildColorTab()
    {
        var element = new UIElement();
        return element;
    }

    private static UIElement BuildAppearanceTab()
    {
        var element = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        // NAME
        var inputField = new InputField("", "");
        element.Append(inputField);

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

        element.Append(maleGrid);
        element.Append(femaleGrid);

        return element;
    }
}
