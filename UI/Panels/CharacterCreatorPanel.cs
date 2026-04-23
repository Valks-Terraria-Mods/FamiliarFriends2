using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace FamiliarFriends;

public class CharacterCreatorPanel : UIState, IClosable
{
    public event Action CloseRequested;

    public override void OnInitialize()
    {
        base.OnInitialize();
        UIPanel panel = new();
        panel.Width.Set(300, 0f);
        panel.Height.Set(150, 0f);
        panel.BackgroundColor = Main.OurFavoriteColor;
        panel.HAlign = 0f;
        panel.VAlign = 0f;
        Append(panel);

        UIButton<string> closeButton = UiControlFactory.CloseBtn();
        closeButton.OnLeftClick += (_, _) => CloseRequested?.Invoke();
        panel.Append(closeButton);
    }
}
