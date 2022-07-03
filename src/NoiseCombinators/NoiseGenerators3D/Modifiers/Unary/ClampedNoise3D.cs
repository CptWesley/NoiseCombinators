﻿using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a noise modifier which changes the range of a given noise generator by clamping.
/// </summary>
public sealed class ClampedNoise3D : UnaryValueMapNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClampedNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to clamp.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    public ClampedNoise3D(INoise3D source, double min, double max)
        : base(source)
    {
        Min = min;
        Max = max;
    }

    /// <inheritdoc/>
    public override sealed double Min { get; }

    /// <inheritdoc/>
    public override sealed double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
    {
        if (value < Min)
        {
            return Min;
        }

        if (value > Max)
        {
            return Max;
        }

        return value;
    }
}
