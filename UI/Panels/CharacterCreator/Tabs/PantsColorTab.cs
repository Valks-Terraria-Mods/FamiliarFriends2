using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class PantsColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.PantsColor;
    protected override Color Color => Player.pantsColor;
    protected override string Title { get; } = "Pants Color";
}
