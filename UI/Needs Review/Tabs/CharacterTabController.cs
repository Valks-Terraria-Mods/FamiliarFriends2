using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public sealed class CharacterTabController(UIElement contentHost)
{
    private readonly UIElement _contentHost = contentHost;
    private readonly Dictionary<CharacterCreatorTab, UIColoredImageButton> _buttons = [];
    private readonly Dictionary<CharacterCreatorTab, UIElement> _contentByTab = [];

    public event Action<CharacterCreatorTab> TabChanged;

    public void AddTab(HBoxContainer row, CharacterCreatorTab tab, Asset<Texture2D> icon, UIElement content, Asset<Texture2D> middle = null)
    {
        UIColoredImageButton button = new(icon);
        button.SetVisibility(1f, 0.5f);

        if (middle != null)
            button.SetMiddleTexture(middle);

        button.OnLeftMouseDown += (_, _) => SelectTab(tab);
        row.Append(button);

        _buttons[tab] = button;
        _contentByTab[tab] = content;
    }

    public void SelectTab(CharacterCreatorTab tab, bool playSound = true)
    {
        if (!_contentByTab.TryGetValue(tab, out UIElement content))
            return;

        if (playSound)
            SoundEngine.PlaySound(SoundID.MenuTick);

        _contentHost.RemoveAllChildren();
        _contentHost.Append(content);

        foreach (KeyValuePair<CharacterCreatorTab, UIColoredImageButton> pair in _buttons)
            pair.Value.SetSelected(pair.Key == tab);

        TabChanged?.Invoke(tab);
    }

    public void SetTabColor(CharacterCreatorTab tab, Color color)
    {
        if (_buttons.TryGetValue(tab, out UIColoredImageButton button))
            button.SetColor(color);
    }

    public void SetTabIcon(CharacterCreatorTab tab, Asset<Texture2D> icon)
    {
        if (_buttons.TryGetValue(tab, out UIColoredImageButton button))
            button.SetImageWithoutSettingSize(icon);
    }
}
