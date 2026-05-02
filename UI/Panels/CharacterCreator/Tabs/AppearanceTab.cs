using FamiliarFriends;
using FamiliarFriends.NPCs;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class AppearanceTab(UIElement parent, Player player) : CharacterTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.Appearance;
    protected override bool NoTitle { get; } = true;

    protected override void BuildContent(UIElement element, VBoxContainer vbox)
    {
        var inputField = new InputField("Name:");

        var spawnBtn = new Button("Spawn")
        {
            PaddingLeft = 40,
            PaddingRight = 40,
            VAlign = 1.0f
        };

        spawnBtn.OnLeftClick += (_, _) => SpawnFriend(PlayerRender);

        vbox.Append(inputField);
        element.Append(spawnBtn);
    }

    private static void SpawnFriend(Player playerRender)
    {
        Player localPlayer = Main.LocalPlayer;

        int npcIndex = NPC.NewNPC(
            localPlayer.GetSource_FromThis(),
            (int)localPlayer.Center.X,
            (int)localPlayer.Center.Y,
            ModContent.NPCType<Friend>()
        );

        if (Main.npc[npcIndex].ModNPC is Friend friend)
        {
            friend.Owner = localPlayer;
            friend.Sprite = playerRender;
        }
    }
}
