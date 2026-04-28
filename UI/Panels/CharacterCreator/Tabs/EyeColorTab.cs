using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class EyeColorTab(UIElement parent) : CharacterColorTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.EyeColor;
    protected override string Title { get; } = "Eye Color";
}
