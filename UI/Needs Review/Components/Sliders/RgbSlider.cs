using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class RgbSlider : MultiColorSlider
{
    private const float SliderHeightPixels = 132f;
    private const float LayoutSpacingPixels = 6f;
    private const float ControlGapHeightPixels = 4f;
    private const float BottomRowHeightPixels = 32f;
    private const float BottomRowSpacingPixels = 4f;
    private const float ButtonHostWidthPixels = 120f;
    private const float ColorSwatchWidthPixels = 58f;
    private const float HexPanelWidthPixels = 120f;
    private const float SwatchBorderLightenFactor = 0.35f;

    private readonly Slider _redSlider;
    private readonly Slider _greenSlider;
    private readonly Slider _blueSlider;

    private readonly UIPanel _colorSwatch;
    private readonly UIText _hexText;
    private readonly UIColoredImageButton _copyButton;
    private readonly UIColoredImageButton _pasteButton;
    private readonly UIColoredImageButton _randomizeButton;

    private bool _suppressNotifications;
    private float _red;
    private float _green;
    private float _blue;

    public RgbSlider(Color initialColor)
    {
        Width.Set(0f, 1f);
        Height.Set(SliderHeightPixels, 0f);

        VBoxContainer layout = new()
        {
            Spacing = LayoutSpacingPixels,
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
        };
        Append(layout);

        _redSlider = CreateChannelSlider(0f, HandleRedChanged, point => ComposeColor(point, 0f, 0f));
        _greenSlider = CreateChannelSlider(0f, HandleGreenChanged, point => ComposeColor(0f, point, 0f));
        _blueSlider = CreateChannelSlider(0f, HandleBlueChanged, point => ComposeColor(0f, 0f, point));

        layout.Append(_redSlider);
        layout.Append(_greenSlider);
        layout.Append(_blueSlider);

        UIElement controlGap = new()
        {
            Height = StyleDimension.FromPixels(ControlGapHeightPixels),
        };
        layout.Append(controlGap);

        HBoxContainer bottomRow = new()
        {
            Spacing = BottomRowSpacingPixels,
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixels(BottomRowHeightPixels),
        };
        layout.Append(bottomRow);

        UIElement buttonHost = new()
        {
            Width = StyleDimension.FromPixels(ButtonHostWidthPixels),
            Height = StyleDimension.FromPixels(BottomRowHeightPixels),
        };
        bottomRow.Append(buttonHost);

        _copyButton = CreateActionButton(CharacterCreationAssets.Actions.Copy, 0f, CopyHexToClipboard);
        _pasteButton = CreateActionButton(CharacterCreationAssets.Actions.Paste, 40f, PasteHexFromClipboard);
        _randomizeButton = CreateActionButton(CharacterCreationAssets.Actions.Randomize, 80f, RandomizeChannels);

        buttonHost.Append(_copyButton);
        buttonHost.Append(_pasteButton);
        buttonHost.Append(_randomizeButton);

        _colorSwatch = new UIPanel
        {
            Width = StyleDimension.FromPixels(ColorSwatchWidthPixels),
            Height = StyleDimension.FromPixels(BottomRowHeightPixels),
        };
        _colorSwatch.SetPadding(0f);
        bottomRow.Append(_colorSwatch);

        UIElement spacer = new()
        {
            Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
            Height = StyleDimension.FromPixels(BottomRowHeightPixels),
        };
        bottomRow.Append(spacer);

        UIPanel hexPanel = new()
        {
            Width = StyleDimension.FromPixels(HexPanelWidthPixels),
            Height = StyleDimension.FromPixels(BottomRowHeightPixels),
        };
        hexPanel.SetPadding(0f);
        bottomRow.Append(hexPanel);

        _hexText = new UIText("#000000")
        {
            HAlign = 0.5f,
            VAlign = 0.5f,
        };
        hexPanel.Append(_hexText);

        SetColor(initialColor, notify: false);
    }

    public void SetColor(Color color, bool notify = true)
    {
        _red = color.R / 255f;
        _green = color.G / 255f;
        _blue = color.B / 255f;

        RunWithoutNotifications(() =>
        {
            _redSlider.SetValue(_red, notify: false);
            _greenSlider.SetValue(_green, notify: false);
            _blueSlider.SetValue(_blue, notify: false);
        });

        ApplyCurrentValues(notify);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        if (ContainsPoint(Main.MouseScreen) && Main.LocalPlayer is not null)
            Main.LocalPlayer.mouseInterface = true;

        HsvSliderTooltipRenderer.DrawIfHovering(
            spriteBatch,
            _copyButton.IsMouseHovering,
            _pasteButton.IsMouseHovering,
            _randomizeButton.IsMouseHovering);
    }

    private Slider CreateChannelSlider(float initialValue, Action<float> onChanged, Func<float, Color> gradientAt)
    {
        Slider slider = new(initialValue, 0f, 1f);
        slider.Width.Set(0f, 1f);
        slider.GradientColorAt = gradientAt;
        slider.ValueChanged += value =>
        {
            if (_suppressNotifications)
                return;

            onChanged(value);
        };

        return slider;
    }

    private static UIColoredImageButton CreateActionButton(ReLogic.Content.Asset<Texture2D> icon, float leftPixels, Action action)
    {
        UIColoredImageButton button = new(icon, isSmall: true)
        {
            Left = StyleDimension.FromPixels(leftPixels),
            VAlign = 0f,
            HAlign = 0f,
        };

        button.SetVisibility(1f, 0.55f);
        button.OnLeftMouseDown += (_, _) => action();

        return button;
    }

    private void HandleRedChanged(float value)
    {
        _red = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void HandleGreenChanged(float value)
    {
        _green = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void HandleBlueChanged(float value)
    {
        _blue = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void ApplyCurrentValues(bool notify)
    {
        Color color = ComposeColor(_red, _green, _blue);

        _colorSwatch.BackgroundColor = color;
        _colorSwatch.BorderColor = Color.Lerp(color, Color.White, SwatchBorderLightenFactor);

        _hexText.SetText($"#{color.R:X2}{color.G:X2}{color.B:X2}");
        PublishSelectedColor(color, notify);
    }

    private static Color ComposeColor(float red, float green, float blue)
    {
        return new Color(ChannelToByte(red), ChannelToByte(green), ChannelToByte(blue), 255);
    }

    private static byte ChannelToByte(float channel)
    {
        int scaled = (int)MathF.Round(MathHelper.Clamp(channel, 0f, 1f) * 255f);
        return (byte)Math.Clamp(scaled, 0, 255);
    }

    private void CopyHexToClipboard() => Platform.Get<IClipboard>().Value = _hexText.Text;

    private void PasteHexFromClipboard()
    {
        string clipboard = Platform.Get<IClipboard>().Value;

        if (TryParseHexColor(clipboard, out Color color))
            SetColor(color, notify: true);
    }

    private void RandomizeChannels()
    {
        _red = Main.rand.NextFloat();
        _green = Main.rand.NextFloat();
        _blue = Main.rand.NextFloat();

        RunWithoutNotifications(() =>
        {
            _redSlider.SetValue(_red, notify: false);
            _greenSlider.SetValue(_green, notify: false);
            _blueSlider.SetValue(_blue, notify: false);
        });

        ApplyCurrentValues(notify: true);
    }

    private void RunWithoutNotifications(Action action)
    {
        _suppressNotifications = true;

        try
        {
            action();
        }
        finally
        {
            _suppressNotifications = false;
        }
    }
}
