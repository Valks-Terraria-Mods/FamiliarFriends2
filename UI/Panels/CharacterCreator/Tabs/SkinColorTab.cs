using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class SkinColorTab(UIElement parent, Player player) : CharacterColorTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.SkinColor;
    protected override Color Color => Player.skinColor;
    protected override string Title { get; } = "Skin Color";
}
