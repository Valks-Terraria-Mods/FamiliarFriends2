using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class CharacterPreview : UIElement
{
    private readonly Player _previewPlayer;
    private readonly UICharacter _previewCharacter;

    private string _displayName = "Friend";

    public CharacterPreview()
    {
        Width.Set(80f, 0f);
        Height.Set(80f, 0f);
        HAlign = 0.5f;
        Top.Set(-71f, 0f);

        _previewPlayer = new Player();
        _previewCharacter = new UICharacter(_previewPlayer, animated: true, hasBackPanel: false, 1.45f)
        {
            Width = StyleDimension.FromPixels(80f),
            Height = StyleDimension.FromPixels(80f),
            HAlign = 0.5f,
            VAlign = 0.5f,
        };

        Append(_previewCharacter);
    }

    public void SetDisplayName(string name)
    {
        _displayName = string.IsNullOrWhiteSpace(name) ? "Friend" : name;
    }

    public void Sync()
    {
        // FriendAppearance appearance = CharacterAppearanceBinding.GetAppearance();
        // appearance.ApplyToPlayer(_previewPlayer);
        // _previewPlayer.skinVariant = FriendAppearance.ResolveSkinVariant(appearance.SkinVariant);
        // _previewPlayer.Male = appearance.IsMale;
        // _previewPlayer.position = Vector2.Zero;
        // _previewPlayer.velocity = Vector2.Zero;
        // _previewPlayer.direction = 1;
        // _previewPlayer.gravDir = 1f;
        // _previewPlayer.socialIgnoreLight = true;
        // _previewPlayer.isFullbright = true;
        // _previewPlayer.name = _displayName;

        // Main.instance.LoadHair(_previewPlayer.hair);
    }
}
