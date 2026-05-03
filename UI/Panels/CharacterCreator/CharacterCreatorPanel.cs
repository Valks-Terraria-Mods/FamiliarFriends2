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

public class CharacterCreatorPanel : UIState, IBlocksInput, IHasCloseButton, IHasMainPanel
{
    public UIElement MainElement { get; set; }

    private readonly Dictionary<CharacterTabType, CharacterTab> _tabs = [];

    public override void OnInitialize()
    {
        base.OnInitialize();

        var localPlayer = Main.LocalPlayer;
        var player = new Player
        {
            hair = localPlayer.hair,
            hairColor = localPlayer.hairColor,
            skinColor = localPlayer.skinColor,
            skinVariant = localPlayer.skinVariant,
            eyeColor = localPlayer.eyeColor,
            shirtColor = localPlayer.shirtColor,
            underShirtColor = localPlayer.underShirtColor,
            pantsColor = localPlayer.pantsColor,
            shoeColor = localPlayer.shoeColor
        };

        MainElement.Width.Set(600, 0);
        MainElement.Height.Set(350, 0);

        Append(MainElement);

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

        MainElement.Append(layout);

        _tabs[CharacterTabType.Appearance].Select();
    }

    public static UIPanel BuildHsvSlider()
    {
        var sliderPanel = new UIPanel()
        {
            Width = StyleDimension.FromPercent(0.6f),
            Height = StyleDimension.FromPixels(152f),
            HAlign = 0.5f,
            VAlign = 0.5f,
            BackgroundColor = ValkyrieAPI.UI.Colors.DarkBackground
        };

        var hsvSlider = new HsvSlider(Color.White);

        sliderPanel.Append(hsvSlider);
        
        return sliderPanel;
    }

    public void SetCloseButton(UIImageButton button)
    {
        MainElement.Append(button);
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
