using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace FamiliarFriends;

public static class CharacterCreationAssets
{
    private const string BasePath = "Images/UI/CharCreation/";

    private static readonly Dictionary<CharCreationTexture, Asset<Texture2D>> _textureCache = [];

    private static readonly CharCreationTexture[] _allTextures = (CharCreationTexture[])Enum.GetValues(typeof(CharCreationTexture));

    public static class Tabs
    {
        public static UIColoredImageButton Appearance => BuildButton(CharCreationTexture.CharInfo);
        public static UIColoredImageButton HairStyles => BuildButton(CharCreationTexture.HairStyle_Hair, CharCreationTexture.HairStyle_Arrow);
        public static UIColoredImageButton GetGenderIcon(Gender gender) => BuildButton(gender == Gender.Male ? CharCreationTexture.ClothStyleMale : CharCreationTexture.ClothStyleFemale);
        public static UIColoredImageButton HairColor => BuildButton(CharCreationTexture.ColorHair);
        public static UIColoredImageButton EyeColor => BuildButton(CharCreationTexture.ColorEye, CharCreationTexture.ColorEyeBack);
        public static UIColoredImageButton SkinColor => BuildButton(CharCreationTexture.ColorSkin);
        public static UIColoredImageButton ShirtColor => BuildButton(CharCreationTexture.ColorShirt);
        public static UIColoredImageButton UnderShirtColor => BuildButton(CharCreationTexture.ColorUndershirt);
        public static UIColoredImageButton PantsColor => BuildButton(CharCreationTexture.ColorPants);
        public static UIColoredImageButton ShoesColor => BuildButton(CharCreationTexture.ColorShoes);
    }

    public static class ColorActions
    {
        public static Asset<Texture2D> Copy => GetTexture(CharCreationTexture.Copy);
        public static Asset<Texture2D> Paste => GetTexture(CharCreationTexture.Paste);
        public static Asset<Texture2D> Randomize => GetTexture(CharCreationTexture.Randomize);
    }

    public static Asset<Texture2D> CategoryPanel => GetTexture(CharCreationTexture.CategoryPanel);
    public static Asset<Texture2D> CategoryPanelBorder => GetTexture(CharCreationTexture.CategoryPanelBorder);
    public static Asset<Texture2D> CategoryPanelHighlight => GetTexture(CharCreationTexture.CategoryPanelHighlight);
    public static Asset<Texture2D> SmallPanel => GetTexture(CharCreationTexture.SmallPanel);
    public static Asset<Texture2D> SmallPanelBorder => GetTexture(CharCreationTexture.SmallPanelBorder);

    public static void EnsureLoaded()
    {
        foreach (var textureKey in _allTextures)
        {
            string path = BasePath + textureKey.ToString();
            _textureCache[textureKey] = Main.Assets.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad);
        }
    }

    private static Asset<Texture2D> GetTexture(CharCreationTexture texture)
    {
        return _textureCache[texture];
    }

    private static UIColoredImageButton BuildButton(CharCreationTexture texture, CharCreationTexture middleTexture = default)
    {
        var btn = new UIColoredImageButton(GetTexture(texture));

        if (middleTexture != default)
            btn.SetMiddleTexture(GetTexture(middleTexture));

        return btn;
    }

    private enum CharCreationTexture
    {
        CategoryPanel,
        CategoryPanelBorder,
        CategoryPanelHighlight,
        SmallPanel,
        SmallPanelBorder,
        Separator1,
        Separator2,
        CharInfo,
        ClothStyleMale,
        ClothStyleFemale,
        HairStyle_Hair,
        HairStyle_Arrow,
        ColorCharacter,
        ColorHair,
        ColorEye,
        ColorEyeBack,
        ColorSkin,
        ColorShirt,
        ColorUndershirt,
        ColorPants,
        ColorShoes,
        Copy,
        Paste,
        Randomize,
        PanelGrayscale,
        Voice
    }
}

public enum Gender
{
    Male,
    Female
}
