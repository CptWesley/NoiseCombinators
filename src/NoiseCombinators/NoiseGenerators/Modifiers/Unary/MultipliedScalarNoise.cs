﻿using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a combinator which multiplies a scalar with another noise generator.
/// </summary>
public sealed class MultipliedScalarNoise : UnaryValueMapNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultipliedScalarNoise"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="scalar">The scalar to add.</param>
    public MultipliedScalarNoise(INoise source, double scalar)
        : base(source)
    {
        double mi = source.Min * scalar;
        double ma = source.Max * scalar;
        Scalar = scalar;

        if (mi > ma)
        {
            Max = mi;
            Min = ma;
        }
        else
        {
            Max = ma;
            Min = mi;
        }
    }

    /// <summary>
    /// Gets the scalar multiplied with every value.
    /// </summary>
    public double Scalar { get; }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => value * Scalar;
}
