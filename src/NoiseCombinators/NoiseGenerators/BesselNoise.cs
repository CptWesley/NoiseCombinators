using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// A noise generator using a two dimensional version of Bessel interpolation.
/// </summary>
public sealed class BesselNoise : BicubicNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BesselNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BesselNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BesselNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BesselNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BesselNoise"/> class.
    /// </summary>
    public BesselNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1, double x2, double x3)
    {
        double d1x0 = x1 - x0;
        double d1x1 = x2 - x1;
        double d1x2 = x3 - x2;

        double d2x0 = d1x1 - d1x0;
        double d2x1 = d1x2 - d1x1;

        double d3x0 = d2x1 - d2x0;

        double tt1 = t * (t - 1);
        double th = t - 0.5;

        double p1 = (x1 + x2) / 2;
        double p2 = th * d1x1;
        double p3 = (tt1 * (d2x0 + d2x1)) / 4;
        double p4 = (th * tt1 * d3x0) / 6;

        double result = p1 + p2 + p3 + p4;

        return result;
    }
}
