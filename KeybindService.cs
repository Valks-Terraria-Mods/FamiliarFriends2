using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace FamiliarFriends;

public class KeybindService
{
    private readonly List<Keybind> _keybinds = [];
    private Mod _mod;

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

    public void Register(string name, string defaultKey, Action action)
    {
        _keybinds.Add(new Keybind(_mod, name, defaultKey, action));
    }
}
