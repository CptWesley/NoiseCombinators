using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// A noise generator using a two dimensional version of cubic interpolation.
/// </summary>
public sealed class BicubicNoise : BicubicNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BicubicNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BicubicNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoise"/> class.
    /// </summary>
    public BicubicNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override double Max => 1.625;

    /// <inheritdoc/>
    public override double Min => -0.625;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1, double x2, double x3)
    {
        double t2 = t * t;
        double t3 = t2 * t;

        double p = (x3 - x2) - (x0 - x1);
        double q = x0 - x1 - p;
        double r = x2 - x0;
        double s = x1;

        double result = (p * t3) + (q * t2) + (r * t) + s;
        return result;
    }
}
