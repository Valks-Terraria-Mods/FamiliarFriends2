using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ValkyrieLib;

namespace FamiliarFriends;

public static class GameAssets
{
    private static readonly Dictionary<GameTexture, Asset<Texture2D>> _textureCache = [];

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

    private static Asset<Texture2D> GetTexture(GameTexture texture)
    {
        const string BasePath = "Images/UI/CharCreation/";
        
        if (!_textureCache.TryGetValue(texture, out var asset))
        {
            asset = Main.Assets.Request<Texture2D>(BasePath + texture.ToString(), AssetRequestMode.ImmediateLoad);
            _textureCache[texture] = asset;
        }
        
        return asset;
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
        ColorShoes
    }
}

public enum Gender
{
    Male,
    Female
}
