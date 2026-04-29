using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class HairStylesTab(UIElement parent, Player player) : CharacterTab(parent, player)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.HairStyles;
    protected override Color Color => Player.hairColor;
    protected override string Title { get; } = "Hair Styles";

    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        // TODO
    }
}
