using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public abstract class CharacterColorTab(UIElement parent) : CharacterTab(parent)
{
    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        content.Append(CharacterCreatorPanel.BuildHsvSlider());
    }
}
