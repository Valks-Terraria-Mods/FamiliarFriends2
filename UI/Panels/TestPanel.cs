using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public class TestPanel : UIState
{
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
    }
}

public class TestPanel2 : UIState
{
    public override void OnInitialize()
    {
        base.OnInitialize();
        UIPanel panel = new();
        panel.Width.Set(300, 0f);
        panel.Height.Set(150, 0f);
        panel.BackgroundColor = Main.legendaryModeColor;
        panel.HAlign = 1.0f;
        panel.VAlign = 1.0f;
        Append(panel);
    }
}
