using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace FamiliarFriends;

public class ToggleableUI(string name, Func<UIState> stateFactory)
{
    public bool IsVisible { get; private set; }

    private GameTime _lastUpdateGameTime;
    private readonly UserInterface _userInterface = new();
    private readonly string _id = $"{nameof(FamiliarFriends)}: {name}";

    public void Insert(List<GameInterfaceLayer> layers, int mouseTextIndex)
    {
        layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(_id, Draw, InterfaceScaleType.UI));
    }

    public void Update(GameTime gameTime)
    {
        if (!IsVisible)
            return;

        _lastUpdateGameTime = gameTime;
        _userInterface.Update(gameTime);
    }

    private bool Draw()
    {
        _userInterface.Draw(Main.spriteBatch, _lastUpdateGameTime);
        return true;
    }

    public void Toggle()
    {
        if (IsVisible)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        IsVisible = true;
        _userInterface.SetState(stateFactory.Invoke());
    }

    public void Hide()
    {
        IsVisible = false;
        _userInterface.SetState(null);
    }
}
