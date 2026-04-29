using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace FamiliarFriends;

public class StaticClothStyleButton : UIClothStyleButton
{
    public new readonly static StyleDimension Width = StyleDimension.FromPixels(44);
    public new readonly static StyleDimension Height = StyleDimension.FromPixels(80);

    public static readonly ClothStyle[] MaleStyles = 
    [
        ClothStyle.MaleStarter,
        ClothStyle.MaleGangster,
        ClothStyle.MaleSticker,
        ClothStyle.MaleCoat,
        ClothStyle.MaleDress
    ];

    public static readonly ClothStyle[] FemaleStyles = 
    [
        ClothStyle.FemaleStarter,
        ClothStyle.FemaleGangster,
        ClothStyle.FemaleSticker,
        ClothStyle.FemaleCoat,
        ClothStyle.FemaleDress
    ];

    public enum ClothStyle
    {
        MaleStarter = PlayerVariantID.MaleStarter,
        MaleSticker = PlayerVariantID.MaleSticker,
        MaleGangster = PlayerVariantID.MaleGangster,
        MaleCoat = PlayerVariantID.MaleCoat,
        MaleDress = PlayerVariantID.MaleDress,
        FemaleStarter = PlayerVariantID.FemaleStarter,
        FemaleSticker = PlayerVariantID.FemaleSticker,
        FemaleGangster = PlayerVariantID.FemaleGangster,
        FemaleCoat = PlayerVariantID.FemaleCoat,
        FemaleDress = PlayerVariantID.FemaleDress
    }
    
    private readonly Player _previewPlayer;
    private readonly int _clothStyleId;
    private readonly bool? _forceMale;

    public StaticClothStyleButton(Player player, ClothStyle clothStyleId)
        : this(player, (int)clothStyleId, null)
    {
    }

    public StaticClothStyleButton(Player player, ClothStyle clothStyleId, bool? forceMale)
        : this(player, (int)clothStyleId, forceMale)
    {
    }

    private StaticClothStyleButton(Player player, int clothStyleId, bool? forceMale)
        : base(player, clothStyleId)
    {
        _previewPlayer = player;
        _clothStyleId = clothStyleId;
        _forceMale = forceMale;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Prevent this button receiving any game world lighting
        _previewPlayer.socialIgnoreLight = true;

        base.Draw(spriteBatch);
    }
}
