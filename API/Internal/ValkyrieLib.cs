// ValkyrieLib.cs
using Terraria.ModLoader;

namespace ValkyrieLib;

public class ValkyrieLib : Mod
{
    public override void Load()
    {
        var keybindService = new KeybindService();
        var uiService = new UserInterfaceService(keybindService);
        ValkyrieAPI.Initialize(keybindService, uiService);
    }
}
