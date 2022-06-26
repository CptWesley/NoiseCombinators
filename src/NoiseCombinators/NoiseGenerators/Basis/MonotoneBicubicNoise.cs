using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators.Basis;

/// <summary>
/// A noise generator using a two dimensional version of monotone cubic interpolation.
/// </summary>
public sealed class MonotoneBicubicNoise : BicubicNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MonotoneBicubicNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public MonotoneBicubicNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonotoneBicubicNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public MonotoneBicubicNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonotoneBicubicNoise"/> class.
    /// </summary>
    public MonotoneBicubicNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1, double x2, double x3)
    {
        double s0 = x1 - x0;
        double s1 = x2 - x1;
        double s2 = x3 - x2;

        double f0 = (s0 < 0 && s1 > 0) || (s0 > 0 && s1 < 0) ? 0 : 2 / ((1 / (x2 - x1)) + (1 / (x1 - x0)));
        double f1 = (s1 < 0 && s2 > 0) || (s1 > 0 && s2 < 0) ? 0 : 2 / ((1 / (x3 - x2)) + (1 / (x2 - x1)));

        double ff0 = ((-2 * (f1 + (2 * f0))) / 1) + (6 * (x2 - x1));
        double ff1 = ((2 * ((2 * f1) + f0)) / 1) - (6 * (x2 - x1));

        double d = (ff1 - ff0) / 6;
        double c = ((2 * ff0) - (1 * ff1)) / 2;
        double b = (x2 - x1) - (c * 3) - (d * 7);
        double a = x1 - (b * 1) - (c * 1) - (d * 1);

        double x = 1 + t;
        double xPow2 = x * x;
        double xPow3 = xPow2 * x;

        double result = a + (b * x) + (c * xPow2) + (d * xPow3);

        return result;
    }
}
