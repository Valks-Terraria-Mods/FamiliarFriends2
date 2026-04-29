using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ValkyrieLib;

namespace FamiliarFriends;

public abstract class CharacterTab
{
    public abstract UIColoredImageButton Btn { get; }
    protected virtual Color Color { get; }
    protected Player Player { get; }
    protected virtual string Title { get; }
    protected virtual bool NoTitle { get; }

    private readonly UIElement _parent;
    private readonly UIElement _content;

    protected CharacterTab(UIElement parent, Player player)
    {
        _parent = parent;
        Player = player;

        _content = new UIElement
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };

        var vbox = new VBoxContainer();

        if (!NoTitle)
        {
            var titleText = new UIText(Title)
            {
                Width = StyleDimension.Fill,
                HAlign = 0.5f,
                PaddingTop = 10,
                PaddingBottom = 10
            };

            vbox.Append(titleText);
        }
        
        _content.Append(vbox);
        BuildContent(_content, vbox);

        if (Color != default)
            Btn.SetColor(Color);
        
        Btn.OnLeftClick += (_, _) => Select();
    }

    public void Select()
    {
        Btn.SetSelected(true);
        _parent.RemoveAllChildren();
        _parent.Append(_content);
    }
    
    protected abstract void BuildContent(UIElement content, VBoxContainer vbox);
}
