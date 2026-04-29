using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class ShirtColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.ShirtColor;
    protected override Color Color => Player.shirtColor;
    protected override string Title { get; } = "Shirt Color";
}
