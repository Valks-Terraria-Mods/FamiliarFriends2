using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class ShoesColorTab(UIElement parent) : CharacterColorTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.ShoesColor;
    protected override string Title { get; } = "Shoes Color";
}
