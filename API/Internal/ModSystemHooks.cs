using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;

namespace FamiliarFriends;

internal class ModSystemHooks : ModSystem
{
    private readonly KeybindService _keybindService = new();
    private UserInterfaceRegistry _uiRegistry;
    private UserInterfaceService _uiService;

    public override void Load()
    {
        _keybindService.Load(Mod);
        _uiService = new UserInterfaceService(_keybindService);
        _uiRegistry = new UserInterfaceRegistry(_uiService);
        _uiRegistry.RegisterAll();
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
