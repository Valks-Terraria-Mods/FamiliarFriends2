using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;

namespace FamiliarFriends;

public class ModSystemHooks : ModSystem
{
    private readonly KeybindService _keybindService = new();
    private UserInterfaceService _uiService;

    public override void Load()
    {
        _uiService = new UserInterfaceService(_keybindService);
        _keybindService.Load(Mod);

        UserInterfaceRegistry.RegisterAll(_uiService);
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
