﻿using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a noise modifier which changes the range of a given noise generator.
/// </summary>
public sealed class RangedNoise3D : UnaryValueMapNoiseBase3D
{
    private readonly double sourceMin;
    private readonly double sourceMax;
    private readonly double sourceRange;
    private readonly double range;

    /// <summary>
    /// Initializes a new instance of the <see cref="RangedNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to change the range of.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    public RangedNoise3D(INoise3D source, double min, double max)
        : base(source)
    {
        sourceMin = source.Min;
        sourceMax = source.Max;
        sourceRange = sourceMax - sourceMin;
        Min = min;
        Max = max;
        range = max - min;
    }

    /// <inheritdoc/>
    public override sealed double Min { get; }

    /// <inheritdoc/>
    public override sealed double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => (((value - sourceMin) * range) / sourceRange) + Min;
}
