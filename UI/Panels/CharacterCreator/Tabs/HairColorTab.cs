using FamiliarFriends;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class HairColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.HairColor;
    protected override Color Color => Player.hairColor;
    protected override string Title { get; } = "Hair Color";
}
