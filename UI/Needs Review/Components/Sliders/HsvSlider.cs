using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FamiliarFriends;

public sealed class HsvSlider : MultiColorSlider
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

    private readonly Slider _hueSlider;
    private readonly Slider _saturationSlider;
    private readonly Slider _valueSlider;

    private readonly UIPanel _colorSwatch;
    private readonly UIText _hexText;
    private readonly UIColoredImageButton _copyButton;
    private readonly UIColoredImageButton _pasteButton;
    private readonly UIColoredImageButton _randomizeButton;

    private bool _suppressNotifications;
    private float _hue;
    private float _saturation;
    private float _value;

    public HsvSlider(Color initialColor)
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

        _hueSlider = CreateChannelSlider(0f, HandleHueChanged, point => HsvColorConverter.HsvToRgb(point, 1f, 1f));
        _saturationSlider = CreateChannelSlider(0f, HandleSaturationChanged, point => HsvColorConverter.HsvToRgb(_hue, point, _value));
        _valueSlider = CreateChannelSlider(0f, HandleValueChanged, point => HsvColorConverter.HsvToRgb(_hue, _saturation, point));

        layout.Append(_hueSlider);
        layout.Append(_saturationSlider);
        layout.Append(_valueSlider);

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

        _copyButton = SetupActionButton(GameAssets.ColorActions.Copy, 0f, CopyHexToClipboard);
        _pasteButton = SetupActionButton(GameAssets.ColorActions.Paste, 40f, PasteHexFromClipboard);
        _randomizeButton = SetupActionButton(GameAssets.ColorActions.Randomize, 80f, RandomizeChannels);

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
        HsvColorConverter.RgbToHsv(color, out _hue, out _saturation, out _value);

        RunWithoutNotifications(() =>
        {
            _hueSlider.SetValue(_hue, notify: false);
            _saturationSlider.SetValue(_saturation, notify: false);
            _valueSlider.SetValue(_value, notify: false);
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

    private void HandleHueChanged(float value)
    {
        _hue = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void HandleSaturationChanged(float value)
    {
        _saturation = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void HandleValueChanged(float value)
    {
        _value = MathHelper.Clamp(value, 0f, 1f);
        ApplyCurrentValues(notify: true);
    }

    private void ApplyCurrentValues(bool notify)
    {
        Color color = HsvColorConverter.HsvToRgb(_hue, _saturation, _value);

        _colorSwatch.BackgroundColor = color;
        _colorSwatch.BorderColor = Color.Lerp(color, Color.White, SwatchBorderLightenFactor);

        _hexText.SetText($"#{color.R:X2}{color.G:X2}{color.B:X2}");
        PublishSelectedColor(color, notify);
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
        _hue = Main.rand.NextFloat();
        _saturation = Main.rand.NextFloat();
        _value = Main.rand.NextFloat();

        RunWithoutNotifications(() =>
        {
            _hueSlider.SetValue(_hue, notify: false);
            _saturationSlider.SetValue(_saturation, notify: false);
            _valueSlider.SetValue(_value, notify: false);
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
