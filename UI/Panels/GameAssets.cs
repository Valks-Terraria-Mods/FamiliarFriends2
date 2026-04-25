using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace FamiliarFriends;

public static class GameAssets
{
    private const string BasePath = "Images/UI/CharCreation/";

    private static readonly Dictionary<GameTexture, Asset<Texture2D>> _textureCache = [];

    private static readonly GameTexture[] _allTextures = (GameTexture[])Enum.GetValues(typeof(GameTexture));

    public static class Tabs
    {
        public static UIColoredImageButton Appearance => BuildButton(GameTexture.CharInfo);
        public static UIColoredImageButton HairStyles => BuildButton(GameTexture.HairStyle_Hair, GameTexture.HairStyle_Arrow);
        public static UIColoredImageButton GetGenderIcon(Gender gender) => BuildButton(gender == Gender.Male ? GameTexture.ClothStyleMale : GameTexture.ClothStyleFemale);
        public static UIColoredImageButton HairColor => BuildButton(GameTexture.ColorHair);
        public static UIColoredImageButton EyeColor => BuildButton(GameTexture.ColorEye, GameTexture.ColorEyeBack);
        public static UIColoredImageButton SkinColor => BuildButton(GameTexture.ColorSkin);
        public static UIColoredImageButton ShirtColor => BuildButton(GameTexture.ColorShirt);
        public static UIColoredImageButton UnderShirtColor => BuildButton(GameTexture.ColorUndershirt);
        public static UIColoredImageButton PantsColor => BuildButton(GameTexture.ColorPants);
        public static UIColoredImageButton ShoesColor => BuildButton(GameTexture.ColorShoes);
    }

    public static class ColorActions
    {
        public static UIColoredImageButton Copy => BuildSmallButton(GameTexture.Copy);
        public static UIColoredImageButton Paste => BuildSmallButton(GameTexture.Paste);
        public static UIColoredImageButton Randomize => BuildSmallButton(GameTexture.Randomize);
    }

    public static Asset<Texture2D> CategoryPanel => GetTexture(GameTexture.CategoryPanel);
    public static Asset<Texture2D> CategoryPanelBorder => GetTexture(GameTexture.CategoryPanelBorder);
    public static Asset<Texture2D> CategoryPanelHighlight => GetTexture(GameTexture.CategoryPanelHighlight);
    public static Asset<Texture2D> SmallPanel => GetTexture(GameTexture.SmallPanel);
    public static Asset<Texture2D> SmallPanelBorder => GetTexture(GameTexture.SmallPanelBorder);

    public static void EnsureLoaded()
    {
        foreach (var textureKey in _allTextures)
        {
            string path = BasePath + textureKey.ToString();
            _textureCache[textureKey] = Main.Assets.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad);
        }
    }

    private static Asset<Texture2D> GetTexture(GameTexture texture)
    {
        return _textureCache[texture];
    }

    private static UIColoredImageButton BuildSmallButton(GameTexture texture)
    {
        return new UIColoredImageButton(GetTexture(texture), isSmall: true);
    }

    private static UIColoredImageButton BuildButton(GameTexture texture, GameTexture middleTexture = default)
    {
        var btn = new UIColoredImageButton(GetTexture(texture));

        if (middleTexture != default)
            btn.SetMiddleTexture(GetTexture(middleTexture));

        return btn;
    }

    private enum GameTexture
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
