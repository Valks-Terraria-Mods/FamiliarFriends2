using Terraria;
using Terraria.ModLoader;
using ValkyrieLib;

namespace FamiliarFriends;

public class FamiliarFriends : Mod
{
    public override void Load()
    {
        ModHandle modHandle = ValkyrieAPI.GetHandle(this);

        modHandle.RegisterUI("Character Creator", "H", () => new CharacterCreatorPanel());
        modHandle.RegisterKeybind("Hello", "K", () => Main.NewText("Hello!"));
    }
}
