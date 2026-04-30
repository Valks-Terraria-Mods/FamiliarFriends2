using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class ShoesColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.ShoesColor;
    protected override Color Color => PlayerRender.shoeColor;
    protected override string Title { get; } = "Shoes Color";
}
