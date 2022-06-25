using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators;

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

        double m0;
        double m1;

        if (BitUtilities.NearlyEqual(x1, x2))
        {
            m0 = 0;
            m1 = 0;
        }
        else
        {
            if (BitUtilities.NearlyEqual(x0, x1) || (s0 < 0 && s1 >= 0) || (s0 > 0 && s1 <= 0))
            {
                m0 = 0;
            }
            else
            {
                m0 = (s0 + s1) * 0.5;
                m0 *= Math.Min(3 * s0 / m0, Math.Min(3 * s1 * m0, 1));
            }

            if (BitUtilities.NearlyEqual(x1, x2) || (s1 < 0 && s2 >= 0) || (s1 > 0 && s2 <= 0))
            {
                m1 = 0;
            }
            else
            {
                m1 = (s1 + s2) * 0.5;
                m1 *= Math.Min(3 * s1 / m1, Math.Min(3 * s2 * m1, 1));
            }
        }

        double result = ((((((m0 + m1 - (2.0 * s1)) * t) + ((3.0 * s1) - (2.0 * m0) - m1)) * t) + m0) * t) + x1;

        double min = Math.Min(x1, x2);
        double max = Math.Max(x1, x2);

        if (result < min)
        {
            return min;
        }

        if (result > max)
        {
            return max;
        }

        return result;
    }
}
