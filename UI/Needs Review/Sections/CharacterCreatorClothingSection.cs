using System;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class CharacterCreatorClothingSection : UIElement
{
    public static readonly int[] MaleStyles = [0, 2, 1, 3, 8];
    public static readonly int[] FemaleStyles = [4, 6, 5, 7, 9];

    private readonly Action<bool> _onGenderRequested;
    private readonly Action _onStyleApplied;
    private readonly List<(UIColoredImageButton Male, UIColoredImageButton Female)> _genderButtons = [];

    public CharacterCreatorClothingSection(Action<bool> onGenderRequested, Action onStyleApplied, bool showHeader = true)
    {
        _onGenderRequested = onGenderRequested;
        _onStyleApplied = onStyleApplied;

        Width.Set(0f, 1f);
        Height.Set(0f, 1f);
        SetPadding(0f);

        VBoxContainer layout = new()
        {
            Spacing = 6f,
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
        };
        Append(layout);

        if (showHeader)
        {
            UIText title = new("Clothing Style")
            {
                HAlign = 0.5f,
                Top = StyleDimension.FromPixels(2f),
            };
            layout.Append(title);
        }

        HBoxContainer styleGroups = new()
        {
            Spacing = 12f,
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixels(122f),
        };
        layout.Append(styleGroups);

        styleGroups.Append(CreateClothGroup("Male", MaleStyles, forceMale: true));
        styleGroups.Append(CreateClothGroup("Female", FemaleStyles, forceMale: false));

        layout.Append(CreateGenderToggleRow());

        UIElement spacer = new()
        {
            Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
        };
        layout.Append(spacer);

        //UpdateGenderSelection(Friend.GetSharedAppearance().IsMale);
    }

    public void UpdateGenderSelection(bool isMale)
    {
        for (int i = 0; i < _genderButtons.Count; i++)
        {
            (UIColoredImageButton maleButton, UIColoredImageButton femaleButton) = _genderButtons[i];
            maleButton.SetSelected(isMale);
            femaleButton.SetSelected(!isMale);
        }
    }

    private UIElement CreateClothGroup(string label, int[] styles, bool forceMale)
    {
        UIElement group = new()
        {
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixels(122f),
        };
        group.SetPadding(0f);

        UIText title = new(label)
        {
            HAlign = 0.5f,
        };
        group.Append(title);

        UIElement buttonHost = new()
        {
            Width = StyleDimension.FromPixels(220f),
            Height = StyleDimension.FromPixels(82f),
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(24f),
        };
        group.Append(buttonHost);

        // for (int i = 0; i < styles.Length; i++)
        // {
        //     StaticClothStyleButton button = new(styles[i], forceMale)
        //     {
        //         Left = StyleDimension.FromPixels(i * 44f),
        //         Top = StyleDimension.FromPixels(0f),
        //     };
        //     button.OnLeftMouseDown += (_, _) => _onStyleApplied?.Invoke();
        //     buttonHost.Append(button);
        // }

        return group;
    }

    private HBoxContainer CreateGenderToggleRow()
    {
        HBoxContainer row = new()
        {
            Spacing = 8f,
            Width = StyleDimension.FromPixels(88f),
            Height = StyleDimension.FromPixels(40f),
            HAlign = 0.5f,
        };

        UIColoredImageButton maleButton = GameAssets.Tabs.GetGenderIcon(Gender.Male);
        maleButton.Width.Set(40, 0);
        maleButton.Height.Set(40, 0);
        maleButton.SetVisibility(1f, 0.6f);
        maleButton.OnLeftMouseDown += (_, _) => _onGenderRequested?.Invoke(true);
        row.Append(maleButton);

        UIColoredImageButton femaleButton = GameAssets.Tabs.GetGenderIcon(Gender.Female);
        femaleButton.Width.Set(40, 0);
        femaleButton.Height.Set(40, 0);
        femaleButton.SetVisibility(1f, 0.6f);
        femaleButton.OnLeftMouseDown += (_, _) => _onGenderRequested?.Invoke(false);
        row.Append(femaleButton);

        _genderButtons.Add((maleButton, femaleButton));

        return row;
    }
}
