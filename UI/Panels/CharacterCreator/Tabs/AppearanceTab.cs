using FamiliarFriends;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public class AppearanceTab(UIElement parent) : CharacterTab(parent)
{
    public override UIColoredImageButton Btn { get; } = GameAssets.Tabs.Appearance;
    protected override bool NoTitle { get; } = true;

    protected override void BuildContent(UIElement element, VBoxContainer vbox)
    {
        var inputField = new InputField("Name:");
        var spawnBtn = new UIButton<string>("Spawn")
        {
            ScalePanel = true,
            PaddingTop = 5,
            PaddingBottom = 5,
            PaddingLeft = 40,
            PaddingRight = 40,
            VAlign = 1.0f
        };

        vbox.Append(inputField);
        element.Append(spawnBtn);
    }
}
