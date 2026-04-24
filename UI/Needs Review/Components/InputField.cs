using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace FamiliarFriends;

public class InputField : UIElement
{
    private const int DefaultMaxLength = 20;
    private const float DefaultHeightPixels = 40f;
    private const float DefaultValuePaddingPixels = 10f;
    private const float HoverOverlayAlpha = 0.55f;
    private const int CaretBlinkPeriodTicks = 40;
    private const int CaretVisibleTicks = 20;
    private const float CaretSpacingPixels = 2f;

    private readonly string _placeholder;

    private string _value;
    private bool _hovered;
    private bool _focused;

    public event Action<string> ValueChanged;

    public int MaxLength { get; set; } = DefaultMaxLength;

    public string Value => _value;

    public bool IsFocused => _focused;

    public bool CenterValueText { get; set; }

    public float CenteredValueVerticalOffset { get; set; }

    protected virtual float ValueLeftPadding => DefaultValuePaddingPixels;

    protected virtual float ValueTopPadding => DefaultValuePaddingPixels;

    public InputField(string placeholder, string initialValue)
    {
        _placeholder = placeholder ?? string.Empty;
        Width.Set(0f, 1f);
        Height.Set(DefaultHeightPixels, 0f);
        CenteredValueVerticalOffset = 0f;
        SetValue(initialValue, notify: false);
    }

    public void SetValue(string value, bool notify = true)
    {
        string normalized = NormalizeValue(value);

        if (_value == normalized)
            return;

        _value = normalized;

        if (notify)
            ValueChanged?.Invoke(_value);
    }

    public void ClearFocus()
    {
        _focused = false;
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);
        _focused = true;
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);
        _hovered = true;
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);
        _hovered = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        bool clickedOutside = _focused
            && !ContainsPoint(Main.MouseScreen)
            && (Main.mouseLeft || Main.mouseRight);

        if (clickedOutside)
            _focused = false;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        UpdateFocusedTextInput();

        CalculatedStyle dimensions = GetDimensions();
        DrawBackground(spriteBatch, dimensions);
        DrawPrefixText(spriteBatch, dimensions);
        DrawValueText(spriteBatch, dimensions);

        if (ContainsPoint(Main.MouseScreen))
            Main.LocalPlayer.mouseInterface = true;
    }

    protected void UpdateFocusedTextInput()
    {
        if (!_focused)
            return;

        PlayerInput.WritingText = true;
        Main.instance.HandleIME();
        SetValue(Main.GetInputText(_value));

        KeyboardState keys = Main.keyState;

        if (keys.IsKeyDown(Keys.Enter) || keys.IsKeyDown(Keys.Escape))
            _focused = false;
    }

    protected virtual void DrawBackground(SpriteBatch spriteBatch, CalculatedStyle dimensions)
    {
        int x = (int)dimensions.X;
        int y = (int)dimensions.Y;
        int width = (int)dimensions.Width;
        int height = (int)dimensions.Height;

        Utils.DrawSplicedPanel(spriteBatch, CharacterCreationAssets.CategoryPanel.Value, x, y, width, height, 10, 10, 10, 10, Color.White * HoverOverlayAlpha);

        if (_hovered || _focused)
            Utils.DrawSplicedPanel(spriteBatch, CharacterCreationAssets.CategoryPanelBorder.Value, x, y, width, height, 10, 10, 10, 10, Color.White);
    }

    protected virtual void DrawValueText(SpriteBatch spriteBatch, CalculatedStyle dimensions)
    {
        string valueText = BuildDisplayValueText();
        Color valueColor = string.IsNullOrWhiteSpace(_value) ? Color.Gray : Color.White;

        Vector2 textSize = FontAssets.MouseText.Value.MeasureString(valueText);
        Vector2 valuePos = CenterValueText
            ? new(
                dimensions.X + ((dimensions.Width - textSize.X) * 0.5f),
                dimensions.Y + ((dimensions.Height - textSize.Y) * 0.5f) + CenteredValueVerticalOffset)
            : new((int)dimensions.X + ValueLeftPadding, (int)dimensions.Y + ValueTopPadding);

        Utils.DrawBorderString(spriteBatch, valueText, valuePos, valueColor);

        if (!_focused || Main.GameUpdateCount % CaretBlinkPeriodTicks >= CaretVisibleTicks)
            return;

        Vector2 caretPos = new(valuePos.X + textSize.X + CaretSpacingPixels, valuePos.Y);
        Utils.DrawBorderString(spriteBatch, "|", caretPos, Color.White);
    }

    protected virtual string BuildDisplayValueText()
    {
        return string.IsNullOrWhiteSpace(_value) ? _placeholder : _value;
    }

    protected virtual void DrawPrefixText(SpriteBatch spriteBatch, CalculatedStyle dimensions)
    {
    }

    protected virtual string NormalizeValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        string normalized = value.Replace("\r", string.Empty).Replace("\n", string.Empty);

        if (normalized.Length > MaxLength)
            normalized = normalized[..MaxLength];

        return normalized;
    }
}
