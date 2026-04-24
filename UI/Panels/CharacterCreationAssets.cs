using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace FamiliarFriends;

public static class CharacterCreationAssets
{
    private const string BasePath = "Images/UI/CharCreation/";

    private static readonly Dictionary<CharCreationTexture, Asset<Texture2D>> _textureCache = [];

    private static readonly CharCreationTexture[] _allTextures = (CharCreationTexture[])Enum.GetValues(typeof(CharCreationTexture));

    public static class Tabs
    {
        public static Asset<Texture2D> Appearance => GetTexture(CharCreationTexture.CharInfo);
        public static Asset<Texture2D> HairStyles => GetTexture(CharCreationTexture.HairStyle_Hair);
        public static Asset<Texture2D> GetGenderIcon(Gender gender) => gender == Gender.Male ? ClothStyle.Male : ClothStyle.Female;
        public static Asset<Texture2D> HairColor => GetTexture(CharCreationTexture.ColorHair);
        public static Asset<Texture2D> EyeColor => GetTexture(CharCreationTexture.ColorEye);
        public static Asset<Texture2D> SkinColor => GetTexture(CharCreationTexture.ColorSkin);
        public static Asset<Texture2D> ShirtColor => GetTexture(CharCreationTexture.ColorShirt);
        public static Asset<Texture2D> UnderShirtColor => GetTexture(CharCreationTexture.ColorUndershirt);
        public static Asset<Texture2D> PantsColor => GetTexture(CharCreationTexture.ColorPants);
        public static Asset<Texture2D> ShoesColor => GetTexture(CharCreationTexture.ColorShoes);
    }

    public static class Actions
    {
        public static Asset<Texture2D> Copy => GetTexture(CharCreationTexture.Copy);
        public static Asset<Texture2D> Paste => GetTexture(CharCreationTexture.Paste);
        public static Asset<Texture2D> Randomize => GetTexture(CharCreationTexture.Randomize);
    }

    public static class ClothStyle
    {
        public static Asset<Texture2D> Male => GetTexture(CharCreationTexture.ClothStyleMale);
        public static Asset<Texture2D> Female => GetTexture(CharCreationTexture.ClothStyleFemale);
    }

    public static Asset<Texture2D> CategoryPanel => GetTexture(CharCreationTexture.CategoryPanel);
    public static Asset<Texture2D> CategoryPanelBorder => GetTexture(CharCreationTexture.CategoryPanelBorder);
    public static Asset<Texture2D> CategoryPanelHighlight => GetTexture(CharCreationTexture.CategoryPanelHighlight);
    public static Asset<Texture2D> SmallPanel => GetTexture(CharCreationTexture.SmallPanel);
    public static Asset<Texture2D> SmallPanelBorder => GetTexture(CharCreationTexture.SmallPanelBorder);
    public static Asset<Texture2D> HairStyleArrow => GetTexture(CharCreationTexture.HairStyle_Arrow);
    public static Asset<Texture2D> ColorEyeBack => GetTexture(CharCreationTexture.ColorEyeBack);

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
