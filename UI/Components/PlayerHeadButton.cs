using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace FamiliarFriends;

public class PlayerHeadButton : Button
{
    private const int ButtonSize = 40;
    private const int HairWidth = 40;
    private const int HairHeight = 58;

    private static readonly Rectangle _headFrame = new(0, 0, HairWidth, HairHeight);

    private readonly int _hairIndex;

    public PlayerHeadButton(int hairIndex)
    {
        _hairIndex = hairIndex;
        Width.Set(ButtonSize, 0f);
        Height.Set(ButtonSize, 0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        // base.DrawSelf(spriteBatch);

        // FriendAppearance appearance = Friend.GetSharedAppearance();
        // int skinVariant = FriendAppearance.ResolveSkinVariant(appearance.SkinVariant);
        // int hairIndex = FriendAppearance.ResolveHairIndex(_hairIndex);

        // Main.instance.LoadHair(hairIndex);

        // Asset<Texture2D> headAsset = TextureAssets.Players[skinVariant, PlayerTextureID.Head];
        // Asset<Texture2D> eyeWhitesAsset = TextureAssets.Players[skinVariant, PlayerTextureID.EyeWhites];
        // Asset<Texture2D> eyesAsset = TextureAssets.Players[skinVariant, PlayerTextureID.Eyes];
        // Asset<Texture2D> hairAsset = TextureAssets.PlayerHair[hairIndex];

        // Vector2 pos = GetDimensions().Position();

        // Draw(headAsset, appearance.SkinColor);
        // Draw(eyeWhitesAsset, Color.White);
        // Draw(eyesAsset, appearance.EyeColor);
        // Draw(hairAsset, appearance.HairColor);

        // void Draw(Asset<Texture2D> asset, Color color)
        // {
        //     spriteBatch.Draw(asset.Value, pos, _headFrame, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        // }
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        base.LeftClick(evt);

        // FriendAppearance appearance = Friend.GetSharedAppearance();
        // appearance.HairIndex = FriendAppearance.ResolveHairIndex(_hairIndex);
        // Friend.SetSharedAppearance(appearance);
    }
}
