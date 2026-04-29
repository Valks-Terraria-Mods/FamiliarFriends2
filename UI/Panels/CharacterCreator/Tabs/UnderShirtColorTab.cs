using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class UnderShirtColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.UnderShirtColor;
    protected override Color Color => Player.underShirtColor;
    protected override string Title { get; } = "Under Shirt Color";
}
