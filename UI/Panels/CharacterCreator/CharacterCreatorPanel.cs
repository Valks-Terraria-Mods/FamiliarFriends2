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
    private static Color HsvPanelColor = new(21, 30, 59, 230);

    private readonly Dictionary<CharacterTabType, CharacterTab> _tabs = [];

    public override void OnInitialize()
    {
        base.OnInitialize();

        Player player = new();

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

        var mainContent = new UIElement()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill  
        };

        _tabs.Add(CharacterTabType.Appearance, new AppearanceTab(mainContent, player));
        _tabs.Add(CharacterTabType.Clothing, new ClothingTab(mainContent, player));
        _tabs.Add(CharacterTabType.HairStyles, new HairStylesTab(mainContent, player));
        _tabs.Add(CharacterTabType.HairColor, new HairColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.EyeColor, new EyeColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.SkinColor, new SkinColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.ShirtColor, new ShirtColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.UnderShirtColor, new UnderShirtColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.PantsColor, new PantsColorTab(mainContent, player));
        _tabs.Add(CharacterTabType.ShoesColor, new ShoesColorTab(mainContent, player));

        foreach (var tab in _tabs.Values)
        {
            tabRow.Append(tab.Btn);
            
            tab.Btn.OnLeftClick += (_, _) =>
            {
                foreach (var tab in _tabs.Values)
                    tab.Btn.SetSelected(false);

                tab.Btn.SetSelected(true);
            };
        }

        layout.Append(tabRow);
        layout.Append(mainContent);

        panel.Append(layout);

        _tabs[CharacterTabType.Appearance].Select();

        // CLOSE BUTTON
        var closeButton = UiControlFactory.CloseBtn();
        closeButton.OnLeftClick += (_, _) => CloseRequested?.Invoke();
        panel.Append(closeButton);
        
        MainElement = panel;
    }

    public static UIPanel BuildHsvSlider()
    {
        var sliderPanel = new UIPanel()
        {
            Width = StyleDimension.FromPercent(0.6f),
            Height = StyleDimension.FromPixels(152f),
            HAlign = 0.5f,
            VAlign = 0.5f,
            BackgroundColor = HsvPanelColor,
        };

        var hsvSlider = new HsvSlider(Color.White);

        sliderPanel.Append(hsvSlider);
        
        return sliderPanel;
    }

    private enum CharacterTabType
    {
        Appearance,
        HairStyles,
        Clothing,
        HairColor,
        EyeColor,
        SkinColor,
        ShirtColor,
        UnderShirtColor,
        PantsColor,
        ShoesColor,
    }
}
