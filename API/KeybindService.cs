using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace FamiliarFriends;

public class KeybindService
{
    private readonly List<Keybind> _keybinds = [];
    private Mod _mod;

    /// <summary>
    /// Registers a new keybind that invokes an action.
    /// </summary>
    /// <param name="name">The name of this keybind.</param>
    /// <param name="defaultKey">The key to use for this keybind.</param>
    /// <param name="action">The action this keybind invokes.</param>
    public void Register(string name, string defaultKey, Action action)
    {
        _keybinds.Add(new Keybind(_mod, name, defaultKey, action));
    }

    public void Load(Mod mod)
    {
        _mod = mod;
    }

    public void UpdateUI()
    {
        foreach (Keybind keybind in _keybinds)
        {
            if (keybind.JustPressed)
                keybind.Execute();
        }
    }
}
