// using Microsoft.Xna.Framework;
// using FamiliarFriends.NPCs;

// namespace FamiliarFriends;

// public static class CharacterAppearanceBinding
// {
//     private const string DefaultName = "Friend";

//     public static FriendAppearance GetAppearance()
//     {
//         return Friend.GetSharedAppearance();
//     }

//     public static void SetAppearance(FriendAppearance appearance)
//     {
//         Friend.SetSharedAppearance(appearance);
//     }

//     public static string GetName()
//     {
//         return Friend.GetSharedName();
//     }

//     public static void SetName(string name)
//     {
//         Friend.SetSharedName(NormalizeName(name));
//     }

//     public static bool IsMale()
//     {
//         return GetAppearance().IsMale;
//     }

//     public static void SetGender(bool isMale)
//     {
//         FriendAppearance appearance = GetAppearance();
//         appearance.IsMale = isMale;
//         SetAppearance(appearance);
//     }

//     public static void SetSkinVariant(int skinVariant)
//     {
//         FriendAppearance appearance = GetAppearance();
//         appearance.SkinVariant = FriendAppearance.ResolveSkinVariant(skinVariant);
//         SetAppearance(appearance);
//     }

//     public static Color GetColor(AppearanceColorTarget target)
//     {
//         FriendAppearance appearance = GetAppearance();

//         return target switch
//         {
//             AppearanceColorTarget.Hair => appearance.HairColor,
//             AppearanceColorTarget.Eye => appearance.EyeColor,
//             AppearanceColorTarget.Skin => appearance.SkinColor,
//             AppearanceColorTarget.Shirt => appearance.ShirtColor,
//             AppearanceColorTarget.UnderShirt => appearance.UnderShirtColor,
//             AppearanceColorTarget.Pants => appearance.PantsColor,
//             AppearanceColorTarget.Shoes => appearance.ShoeColor,
//             _ => appearance.HairColor,
//         };
//     }

//     public static void SetColor(AppearanceColorTarget target, Color color)
//     {
//         FriendAppearance appearance = GetAppearance();

//         switch (target)
//         {
//             case AppearanceColorTarget.Hair:
//                 appearance.HairColor = color;
//                 break;
//             case AppearanceColorTarget.Eye:
//                 appearance.EyeColor = color;
//                 break;
//             case AppearanceColorTarget.Skin:
//                 appearance.SkinColor = color;
//                 break;
//             case AppearanceColorTarget.Shirt:
//                 appearance.ShirtColor = color;
//                 break;
//             case AppearanceColorTarget.UnderShirt:
//                 appearance.UnderShirtColor = color;
//                 break;
//             case AppearanceColorTarget.Pants:
//                 appearance.PantsColor = color;
//                 break;
//             case AppearanceColorTarget.Shoes:
//                 appearance.ShoeColor = color;
//                 break;
//         }

//         SetAppearance(appearance);
//     }

//     private static string NormalizeName(string value)
//     {
//         string normalized = value?.Trim();

//         if (string.IsNullOrWhiteSpace(normalized))
//             return DefaultName;

//         return normalized;
//     }
// }
