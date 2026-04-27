using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class CharacterTabPage
{
    public UIColoredImageButton Button { get; }
    public UIElement Content { get; }

    private readonly UIElement _mainContent;

    public CharacterTabPage(UIElement mainContent, UIColoredImageButton btn, UIElement content)
    {
        _mainContent = mainContent;
        Button = btn;
        Content = content;

        btn.OnLeftClick += (_, _) => Select();
    }
    
    public void SetColor(Color color) => Button.SetColor(color);
    public void Select()
    {
        _mainContent.RemoveAllChildren();
        _mainContent.Append(Content);
    }
}
