using log4net.Repository.Hierarchy;
using Terraria;
using Terraria.ModLoader;
using ValkyrieLib;

namespace FamiliarFriends;

public class FamiliarFriends : Mod
{
    private static Mod _instance;

    public override void Load()
    {
        _instance = this;

        CharacterCreationAssets.EnsureLoaded();

        ModHandle modHandle = ValkyrieAPI.GetHandle(this);

        modHandle.RegisterUI("Character Creator", "H", () => new CharacterCreatorPanel());
        modHandle.RegisterKeybind("Hello", "K", () => Main.NewText("Hello!"));
    }

    public static void Log(object message)
    {
        _instance.Logger.Info(message?.ToString());
    }
}
