using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class CharacterCreatorPanel : UIState, IInputConsumer, IClosable
{
    public UIElement MainElement { get; private set; }

    public event Action CloseRequested;

    private static Color BackgroundColor = new(29, 37, 69, 224);
    private static Color BorderColor = new(77, 96, 151);

    private readonly Dictionary<CharacterTab, CharacterTabPage> _tabs = [];

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
        var layout = new VBoxContainer()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        var tabRow = new HBoxContainer()
        {
            Width = StyleDimension.Fill
        };

        var mainContent = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        _tabs.Add(CharacterTab.Appearance, new CharacterTabPage(mainContent, GameAssets.Tabs.Appearance, BuildAppearanceTab()));
        _tabs.Add(CharacterTab.Clothing, new CharacterTabPage(mainContent, GameAssets.Tabs.GetGenderIcon(Gender.Female), BuildClothingTab()));
        _tabs.Add(CharacterTab.HairStyles, new CharacterTabPage(mainContent, GameAssets.Tabs.HairStyles, BuildHairStyleTab()));
        _tabs.Add(CharacterTab.HairColor, new CharacterTabPage(mainContent, GameAssets.Tabs.HairColor, BuildColorTab()));
        _tabs.Add(CharacterTab.EyeColor, new CharacterTabPage(mainContent, GameAssets.Tabs.EyeColor, BuildColorTab()));
        _tabs.Add(CharacterTab.SkinColor, new CharacterTabPage(mainContent, GameAssets.Tabs.SkinColor, BuildColorTab()));
        _tabs.Add(CharacterTab.ShirtColor, new CharacterTabPage(mainContent, GameAssets.Tabs.ShirtColor, BuildColorTab()));
        _tabs.Add(CharacterTab.UnderShirtColor, new CharacterTabPage(mainContent, GameAssets.Tabs.UnderShirtColor, BuildColorTab()));
        _tabs.Add(CharacterTab.PantsColor, new CharacterTabPage(mainContent, GameAssets.Tabs.PantsColor, BuildColorTab()));
        _tabs.Add(CharacterTab.ShoesColor, new CharacterTabPage(mainContent, GameAssets.Tabs.ShoesColor, BuildColorTab()));

        foreach (var tab in _tabs.Values)
            tabRow.Append(tab.Button);

        layout.Append(tabRow);
        layout.Append(mainContent);

        panel.Append(layout);

        _tabs[CharacterTab.Appearance].Select();

        // CLOSE BUTTON
        var closeButton = UiControlFactory.CloseBtn();
        closeButton.OnLeftClick += (_, _) => CloseRequested?.Invoke();
        panel.Append(closeButton);
        
        MainElement = panel;
    }

    private static UIElement BuildAppearanceTab()
    {
        var element = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,
        };

        var topVbox = new VBoxContainer()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        // NAME
        var inputField = new InputField("Name:");
        var spawnBtn = new UIButton<string>("Spawn")
        {
            ScalePanel = true,
            PaddingTop = 5,
            PaddingBottom = 5,
            PaddingLeft = 40,
            PaddingRight = 40,
            VAlign = 1.0f
        };

        topVbox.Append(inputField);

        element.Append(topVbox);
        element.Append(spawnBtn);

        return element;
    }

    private static UIElement BuildClothingTab()
    {
        var element = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

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

    private static UIElement BuildHairStyleTab()
    {
        var element = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        var vbox = new VBoxContainer();

        var title = new UIText("Hair Style")
        {
            Width = StyleDimension.Fill,
            HAlign = 0.5f,
            PaddingTop = 10,
            PaddingBottom = 10
        };

        vbox.Append(title);

        element.Append(vbox);

        return element;
    }

    private static UIElement BuildColorTab()
    {
        var element = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        var sliderPanel = new UIPanel()
        {
            Width = StyleDimension.FromPercent(0.6f),
            Height = StyleDimension.FromPixels(152f),
            HAlign = 0.5f,
            VAlign = 0.5f,
            BackgroundColor = new Color(23, 33, 65) * 0.9f,
        };

        var hsvSlider = new HsvSlider(Color.White);

        sliderPanel.Append(hsvSlider);
        element.Append(sliderPanel);
        
        return element;
    }
}
