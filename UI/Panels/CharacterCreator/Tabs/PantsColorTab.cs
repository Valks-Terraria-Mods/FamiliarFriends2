using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class PantsColorTab(UIElement parent) : CharacterColorTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.PantsColor;
    protected override string Title { get; } = "Pants Color";
}
