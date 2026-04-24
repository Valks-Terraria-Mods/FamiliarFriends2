using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace FamiliarFriends;

public class Slider : UIElement
{
    private readonly UIElement _track;
    private bool _dragging;

    private const float KnobWidthBase = 12f;
    private const float KnobHeightBase = 14f;
    private const float ValueEpsilon = 0.0001f;
    private readonly float _minValue;
    private readonly float _maxValue;
    private readonly float _range;

    private static readonly Color DefaultTrackBorderColor = new(70, 86, 112);
    private static readonly Color DefaultTrackBackgroundColor = new(18, 22, 30);
    private static readonly Color DefaultTrackFillColor = new(255, 255, 255);
    private static readonly Color DefaultKnobFaceColor = new(24, 32, 44);
    private static readonly Color DefaultKnobBorderColor = new(146, 190, 255);
    private static readonly Color DefaultKnobGripColor = new(186, 216, 255);

    public Color TrackBorderColor { get; set; } = DefaultTrackBorderColor;

    public Color TrackBackgroundColor { get; set; } = DefaultTrackBackgroundColor;

    public Color FillColor { get; set; } = DefaultTrackFillColor;

    public Color KnobFaceColor { get; set; } = DefaultKnobFaceColor;

    public Color KnobBorderColor { get; set; } = DefaultKnobBorderColor;

    public Color KnobGripColor { get; set; } = DefaultKnobGripColor;

    public Func<float, Color> GradientColorAt { get; set; }

    /// <summary>
    /// Fired when the slider value changes due to user input.
    /// </summary>
    public event Action<float> ValueChanged;

    /// <summary>
    /// Current value represented by this slider.
    /// </summary>
    public float Value { get; private set; }

    /// <summary>
    /// Creates a float slider row.
    /// </summary>
    public Slider(float initialValue, float minValue, float maxValue)
    {
        const float sliderHeight = 24f;
        const float trackLeft = 5f;
        const float trackRightInset = 10f;
        const float trackHeight = 8f;
        const float trackTop = (sliderHeight - trackHeight) * 0.5f;

        if (maxValue < minValue)
            (minValue, maxValue) = (maxValue, minValue);

        _minValue = minValue;
        _maxValue = maxValue;
        _range = _maxValue - _minValue;

        Width.Set(0, 1f);
        Height.Set(sliderHeight, 0f);

        _track = new UIElement();
        _track.Left.Set(trackLeft, 0f);
        _track.Width.Set(-trackRightInset, 1f);
        _track.Top.Set(trackTop, 0f);
        _track.Height.Set(trackHeight, 0f);
        _track.OnLeftMouseDown += OnTrackMouseDown;
        _track.OnLeftMouseUp += OnTrackMouseUp;
        Append(_track);

        SetValue(initialValue, notify: false);
    }

    /// <summary>
    /// Draws a flat modern slider with constrained knob travel.
    /// </summary>
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        DrawSlider(spriteBatch);

        if (ContainsPoint(Main.MouseScreen))
            Main.LocalPlayer.mouseInterface = true;
    }

    /// <summary>
    /// Continues drag updates while the left mouse button remains held.
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!_dragging)
            return;

        if (!Main.mouseLeft)
        {
            StopDragging();
            return;
        }

        UpdateValueFromMouse();
    }

    /// <summary>
    /// Sets slider value and optionally emits a value-changed callback.
    /// </summary>
    public void SetValue(float value, bool notify)
    {
        float clamped = MathHelper.Clamp(value, _minValue, _maxValue);

        if (Math.Abs(Value - clamped) <= ValueEpsilon)
            return;

        Value = clamped;

        if (notify)
            ValueChanged?.Invoke(Value);
    }

    /// <summary>
    /// Starts dragging from the track and snaps value to cursor position.
    /// </summary>
    private void OnTrackMouseDown(UIMouseEvent evt, UIElement listeningElement)
    {
        _dragging = true;
        UpdateValueFromMouse();
    }

    /// <summary>
    /// Ends active slider drag.
    /// </summary>
    private void OnTrackMouseUp(UIMouseEvent evt, UIElement listeningElement)
    {
        StopDragging();
    }

    private void StopDragging()
    {
        _dragging = false;
    }

    /// <summary>
    /// Converts cursor X position to a clamped value on the slider range.
    /// </summary>
    private void UpdateValueFromMouse()
    {
        CalculatedStyle dimensions = _track.GetDimensions();

        if (dimensions.Width <= 0f)
            return;

        const float knobWidth = KnobWidthBase;

        float sliderMinX = dimensions.X + 1f;
        float sliderWidth = Math.Max(1f, dimensions.Width - 2f);
        float travelWidth = Math.Max(1f, sliderWidth - knobWidth);

        float knobLeft = Main.MouseScreen.X - (knobWidth * 0.5f);
        float t = (knobLeft - sliderMinX) / travelWidth;
        t = MathHelper.Clamp(t, 0f, 1f);

        float value = _range <= 0f ? _minValue : _minValue + (t * _range);
        SetValue(value, notify: true);
    }

    /// <summary>
    /// Draws the slider track, fill, and knob with exact edge clamping.
    /// </summary>
    private void DrawSlider(SpriteBatch spriteBatch)
    {
        SliderGeometryBuildRequest buildRequest = new(_track, Value, _minValue, _range, KnobWidthBase, KnobHeightBase);

        if (!SliderRenderer.TryCreateGeometry(buildRequest, out SliderGeometry geometry))
            return;

        SliderRenderStyle renderStyle = new(
            TrackBorderColor,
            TrackBackgroundColor,
            FillColor,
            KnobBorderColor,
            KnobFaceColor,
            KnobGripColor,
            GradientColorAt);

        SliderRenderer.Draw(
            spriteBatch,
            geometry,
            renderStyle);
    }
}
