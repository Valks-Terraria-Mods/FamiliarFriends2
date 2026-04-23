using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;

namespace ValksTerrariaLib;

internal class ModSystemHooks : ModSystem
{
    private readonly KeybindService _keybindService = new();
    private UserInterfaceService _uiService;

    public override void Load()
    {
        _keybindService.Load(Mod);
        _uiService = new UserInterfaceService(_keybindService);
    }

    public override void UpdateUI(GameTime gameTime)
    {
        base.UpdateUI(gameTime);
        _keybindService.UpdateUI();
        _uiService.UpdateUI(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        base.ModifyInterfaceLayers(layers);
        _uiService.ModifyInterfaceLayers(layers);
    }
}
