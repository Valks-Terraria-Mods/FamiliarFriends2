using Terraria;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public abstract class CharacterColorTab(UIElement parent, Player player) : CharacterTab(parent, player)
{
    protected override void BuildContent(UIElement content, VBoxContainer vbox)
    {
        content.Append(CharacterCreatorPanel.BuildHsvSlider());
    }
}
