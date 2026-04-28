using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class SkinColorTab(UIElement parent) : CharacterColorTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.SkinColor;
    protected override string Title { get; } = "Skin Color";
}
