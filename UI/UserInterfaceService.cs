using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace FamiliarFriends;

public class UserInterfaceService(KeybindService keybindService)
{
    private const string VanillaMouseLayerId = "Vanilla: Mouse Text";
    private readonly List<ToggleableUI> _uis = [];

    public void Add(string keybindName, string keybind, UIState uiState)
    {
        ToggleableUI ui = new(keybindName, () => uiState);
        _uis.Add(ui);
        keybindService.Register(keybindName, keybind, ui.Toggle);
    }

    public void UpdateUI(GameTime gameTime)
    {
        foreach (ToggleableUI ui in _uis)
            ui.Update(gameTime);
    }

    public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int vanillaMouseLayer = layers.FindIndex(layer => layer.Name.Equals(VanillaMouseLayerId, StringComparison.Ordinal));

        if (vanillaMouseLayer == -1)
            return;

        foreach (ToggleableUI ui in _uis)
        {
            if (ui.IsVisible)
            {
                ui.Insert(layers, vanillaMouseLayer);
                vanillaMouseLayer++;
            }
        }
    }
}
