using Terraria.ModLoader;
using ValkyrieLib;

namespace FamiliarFriends;

public class ModSystemHooks : ModSystem
{
    public override void Load()
    {
        ModHandle modHandle = ValkyrieAPI.GetHandle(Mod);

        modHandle.RegisterUI("Character Creator", "H", () => new CharacterCreatorPanel());
    }
}
