using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators.Basis;

/// <summary>
/// Provides a noise generator which applies two dimensional smootherstep interpolation to generate noise.
/// </summary>
public sealed class SmootherstepNoise : BilinearNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public SmootherstepNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public SmootherstepNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise"/> class.
    /// </summary>
    public SmootherstepNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1)
    {
        t = t * t * t * ((t * ((t * 6) - 15)) + 10);
        return (x0 * (1 - t)) + (x1 * t);
    }
}
