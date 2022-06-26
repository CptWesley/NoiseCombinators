using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators.Basis;

/// <summary>
/// Provides a noise generator which applies two dimensional cosine interpolation to generate noise.
/// </summary>
public sealed class CosineNoise : BilinearNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public CosineNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public CosineNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise"/> class.
    /// </summary>
    public CosineNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1)
    {
        double t2 = (1 - Math.Cos(t * Math.PI)) * 0.5;
        return (x0 * (1 - t2)) + (x1 * t2);
    }
}
