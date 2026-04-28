using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class HairStylesTab(UIElement parent) : CharacterTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.HairStyles;
    protected override string Title { get; } = "Hair Styles";

    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        // TODO
    }
}
