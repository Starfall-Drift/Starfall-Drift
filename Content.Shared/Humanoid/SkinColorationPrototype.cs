// SPDX-FileCopyrightText: 2025 pathetic meowmeow <uhhadd@gmail.com>
// SPDX-License-Identifier: MIT

using System.Numerics;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Humanoid;

/// <summary>
/// A prototype containing a SkinColorationStrategy
/// </summary>
[Prototype]
public sealed partial class SkinColorationPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    /// <summary>
    // _Starfall: (OPTIONAL) Display name used when multiple skin types are selectable in the character editor
    /// </summary>
    [DataField]
    public LocId? Name;

    /// <summary>
    /// The skin coloration strategy specified by this prototype
    /// </summary>
    [DataField(required: true)]
    public ISkinColorationStrategy Strategy = default!;
}

/// <summary>
/// The type of input taken by a <see cref="SkinColorationStrategy" />
/// </summary>
[Serializable, NetSerializable]
public enum SkinColorationStrategyInput
{
    /// <summary>
    /// A single floating point number from 0 to 100 (inclusive)
    /// </summary>
    Unary,

    /// <summary>
    /// A <see cref="Color" />
    /// </summary>
    Color,
}

/// <summary>
/// Takes in the given <see cref="SkinColorationStrategyInput" /> and returns an adjusted Color
/// </summary>
public interface ISkinColorationStrategy
{
    /// <summary>
    /// The type of input expected by the implementor; callers should consult InputType before calling the methods that require a given input
    /// </summary>
    SkinColorationStrategyInput InputType { get; }

    /// <summary>
    /// Returns whether or not the provided <see cref="Color" /> is within bounds of this strategy
    /// </summary>
    bool VerifySkinColor(Color color);

    /// <summary>
    /// Returns the closest skin color that this strategy would provide to the given <see cref="Color" />
    /// </summary>
    Color ClosestSkinColor(Color color);

    /// <summary>
    /// Returns the input if it passes <see cref="VerifySkinColor">, otherwise returns <see cref="ClosestSkinColor" />
    /// </summary>
    Color EnsureVerified(Color color)
    {
        if (VerifySkinColor(color))
        {
            return color;
        }

        return ClosestSkinColor(color);
    }

    /// <summary>
    /// Returns a colour representation of the given unary input
    /// </summary>
    Color FromUnary(float unary)
    {
        throw new InvalidOperationException("This coloration strategy does not support unary input");
    }

    /// <summary>
    /// Returns a colour representation of the given unary input
    /// </summary>
    float ToUnary(Color color)
    {
        throw new InvalidOperationException("This coloration strategy does not support unary input");
    }
}

/// <summary>
/// Unary coloration strategy that returns human skin tones, with 0 being lightest and 100 being darkest
/// </summary>
[DataDefinition]
[Serializable, NetSerializable]
public sealed partial class HumanTonedSkinColoration : ISkinColorationStrategy
{
    private const float LightH = 28f, LightS = 35f, LightV = 100f;
    private const float DarkH  = 20f, DarkS  = 80f, DarkV  = 18f;

    [DataField]
    public Color ValidHumanSkinTone = Color.FromHsv(new Vector4(LightH / 360f, LightS / 100f, LightV / 100f, 1f));

    public SkinColorationStrategyInput InputType => SkinColorationStrategyInput.Unary;

    public bool VerifySkinColor(Color color)
    {
        var colorValues = Color.ToHsv(color);

        var hue = colorValues.X * 360f;
        var sat = colorValues.Y * 100f;
        var val = colorValues.Z * 100f;

        if (hue < 15f || hue > 38f)
            return false;

        if (sat < LightS - 5f || sat > DarkS + 5f)
            return false;

        if (val < DarkV - 5f || val > LightV + 5f)
            return false;

        return true;
    }

    public Color ClosestSkinColor(Color color)
    {
        return ValidHumanSkinTone;
    }

    public Color FromUnary(float color)
    {
        // 0 = lightest (peach), 100 = darkest (deep brown)
        // Linear interpolation across all three HSV channels.
        var t = Math.Clamp(color, 0f, 100f) / 100f;

        var hue = LightH + (DarkH - LightH) * t;
        var sat = LightS + (DarkS - LightS) * t;
        var val = LightV + (DarkV - LightV) * t;

        return Color.FromHsv(new Vector4(hue / 360f, sat / 100f, val / 100f, 1.0f));
    }

    public float ToUnary(Color color)
    {
        var hsv = Color.ToHsv(color);
        // Invert the value lerp — value channel spans the widest range
        var val = hsv.Z * 100f;
        var t = (val - LightV) / (DarkV - LightV);
        return Math.Clamp(t * 100f, 0f, 100f);
    }
}

/// <summary>
/// Unary coloration strategy that clamps the color within the HSV colorspace
/// </summary>
[DataDefinition]
[Serializable, NetSerializable]
public sealed partial class ClampedHsvColoration : ISkinColorationStrategy
{
    /// <summary>
    /// The (min, max) of the hue channel.
    /// </summary>
    [DataField]
    public (float, float)? Hue;

    /// <summary>
    /// The (min, max) of the saturation channel.
    /// </summary>
    [DataField]
    public (float, float)? Saturation;

    /// <summary>
    /// The (min, max) of the value channel.
    /// </summary>
    [DataField]
    public (float, float)? Value;

    public SkinColorationStrategyInput InputType => SkinColorationStrategyInput.Color;

    public bool VerifySkinColor(Color color)
    {
        var hsv = Color.ToHsv(color);

        if (Hue is (var minHue, var maxHue) && (hsv.X < minHue || hsv.X > maxHue))
            return false;

        if (Saturation is (var minSaturation, var maxSaturation) && (hsv.Y < minSaturation || hsv.Y > maxSaturation))
            return false;

        if (Value is (var minValue, var maxValue) && (hsv.Z < minValue || hsv.Z > maxValue))
            return false;

        return true;
    }

    public Color ClosestSkinColor(Color color)
    {
        var hsv = Color.ToHsv(color);

        if (Hue is (var minHue, var maxHue))
            hsv.X = Math.Clamp(hsv.X, minHue, maxHue);

        if (Saturation is (var minSaturation, var maxSaturation))
            hsv.Y = Math.Clamp(hsv.Y, minSaturation, maxSaturation);

        if (Value is (var minValue, var maxValue))
            hsv.Z = Math.Clamp(hsv.Z, minValue, maxValue);

        return Color.FromHsv(hsv);
    }
}

/// <summary>
/// Unary coloration strategy that clamps the color within the HSL colorspace
/// </summary>
[DataDefinition]
[Serializable, NetSerializable]
public sealed partial class ClampedHslColoration : ISkinColorationStrategy
{
    /// <summary>
    /// The (min, max) of the hue channel.
    /// </summary>
    [DataField]
    public (float, float)? Hue;

    /// <summary>
    /// The (min, max) of the saturation channel.
    /// </summary>
    [DataField]
    public (float, float)? Saturation;

    /// <summary>
    /// The (min, max) of the lightness channel.
    /// </summary>
    [DataField]
    public (float, float)? Lightness;

    public SkinColorationStrategyInput InputType => SkinColorationStrategyInput.Color;

    public bool VerifySkinColor(Color color)
    {
        var hsl = Color.ToHsl(color);

        if (Hue is (var minHue, var maxHue) && (hsl.X < minHue || hsl.X > maxHue))
            return false;

        if (Saturation is (var minSaturation, var maxSaturation) && (hsl.Y < minSaturation || hsl.Y > maxSaturation))
            return false;

        if (Lightness is (var minValue, var maxValue) && (hsl.Z < minValue || hsl.Z > maxValue))
            return false;

        return true;
    }

    public Color ClosestSkinColor(Color color)
    {
        var hsl = Color.ToHsl(color);

        if (Hue is (var minHue, var maxHue))
            hsl.X = Math.Clamp(hsl.X, minHue, maxHue);

        if (Saturation is (var minSaturation, var maxSaturation))
            hsl.Y = Math.Clamp(hsl.Y, minSaturation, maxSaturation);

        if (Lightness is (var minValue, var maxValue))
            hsl.Z = Math.Clamp(hsl.Z, minValue, maxValue);

        return Color.FromHsl(hsl);
    }
}
