using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class ShirtColorTab(UIElement parent) : CharacterColorTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.ShirtColor;
    protected override string Title { get; } = "Shirt Color";
}
