using System;
using Terraria.ModLoader;

namespace FamiliarFriends;

public class Keybind(Mod mod, string name, string binding, Action action)
{
    private readonly ModKeybind _keybind = KeybindLoader.RegisterKeybind(mod, name, binding);

    public bool JustPressed => _keybind.JustPressed;
    public void Execute() => action();
}
