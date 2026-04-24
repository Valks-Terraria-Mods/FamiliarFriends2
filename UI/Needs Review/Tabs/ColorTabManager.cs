using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class ColorTabManager
{
    private readonly Dictionary<AppearanceColorTarget, List<HsvSlider>> _editors = [];

    private bool _suppressEditorCallbacks;

    public event Action ColorsChanged;

    public ColorTabManager()
    {
        foreach (AppearanceColorTarget target in Enum.GetValues<AppearanceColorTarget>())
            _editors[target] = [];
    }

    public UIElement BuildColorTab(string title, AppearanceColorTarget target)
    {
        UIElement root = new()
        {
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
        };

        UIText heading = new(title)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(8f),
        };
        root.Append(heading);

        UIPanel sliderPanel = new()
        {
            Width = StyleDimension.FromPixels(330f),
            Height = StyleDimension.FromPixels(152f),
            HAlign = 0.5f,
            VAlign = 0.5f,
            Top = StyleDimension.FromPixels(14f),
            BackgroundColor = new Color(23, 33, 65) * 0.9f,
        };
        sliderPanel.SetPadding(10f);
        root.Append(sliderPanel);

        // HsvSlider slider = new(CharacterAppearanceBinding.GetColor(target));
        // slider.Width.Set(0f, 1f);
        // slider.ColorChanged += color => ApplyEditorColor(target, slider, color);
        // sliderPanel.Append(slider);

        // _editors[target].Add(slider);

        return root;
    }

    public void SyncEditorsFromBinding()
    {
        // _suppressEditorCallbacks = true;

        // foreach (AppearanceColorTarget target in _editors.Keys)
        // {
        //     Color color = CharacterAppearanceBinding.GetColor(target);
        //     List<HsvSlider> targetEditors = _editors[target];

        //     for (int i = 0; i < targetEditors.Count; i++)
        //         targetEditors[i].SetColor(color, notify: false);
        // }

        // _suppressEditorCallbacks = false;
    }

    public static void ApplyTabColors(CharacterTabController tabController)
    {
        // tabController.SetTabColor(CharacterCreatorTab.HairStyles, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Hair));
        // tabController.SetTabColor(CharacterCreatorTab.HairColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Hair));
        // tabController.SetTabColor(CharacterCreatorTab.EyeColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Eye));
        // tabController.SetTabColor(CharacterCreatorTab.SkinColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Skin));
        // tabController.SetTabColor(CharacterCreatorTab.ShirtColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Shirt));
        // tabController.SetTabColor(CharacterCreatorTab.UnderShirtColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.UnderShirt));
        // tabController.SetTabColor(CharacterCreatorTab.PantsColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Pants));
        // tabController.SetTabColor(CharacterCreatorTab.ShoesColor, CharacterAppearanceBinding.GetColor(AppearanceColorTarget.Shoes));
    }

    private void ApplyEditorColor(AppearanceColorTarget target, HsvSlider sourceEditor, Color color)
    {
        if (_suppressEditorCallbacks)
            return;

        //CharacterAppearanceBinding.SetColor(target, color);

        _suppressEditorCallbacks = true;

        List<HsvSlider> targetEditors = _editors[target];

        for (int i = 0; i < targetEditors.Count; i++)
        {
            HsvSlider editor = targetEditors[i];

            if (ReferenceEquals(editor, sourceEditor))
                continue;

            editor.SetColor(color, notify: false);
        }

        _suppressEditorCallbacks = false;

        ColorsChanged?.Invoke();
    }
}
