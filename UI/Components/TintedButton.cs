using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace FamiliarFriends;

public class TintedButton : UIElement
{
    private bool _isMouseDown;

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        CalculatedStyle dim = GetDimensions();
        int overlayAlpha = _isMouseDown ? 100 : IsMouseHovering ? 50 : 0;

        if (overlayAlpha > 0)
            DrawOverlayTint(spriteBatch, dim, overlayAlpha);
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);
        _isMouseDown = true;
    }

    public override void LeftMouseUp(UIMouseEvent evt)
    {
        base.LeftMouseUp(evt);
        _isMouseDown = false;
    }

    private static void DrawOverlayTint(SpriteBatch spriteBatch, CalculatedStyle dim, int transparency)
    {
        Vector2 pos = dim.Position();

        spriteBatch.Draw(TextureAssets.MagicPixel.Value,
            new Rectangle((int)pos.X, (int)pos.Y, (int)dim.Width, (int)dim.Height),
            new Color(0, 0, 0, transparency));
    }
}
