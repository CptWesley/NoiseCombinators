using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a noise generator which applies three dimensional cosine interpolation to generate noise.
/// </summary>
public sealed class CosineNoise3D : TrilinearNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public CosineNoise3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public CosineNoise3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosineNoise3D"/> class.
    /// </summary>
    public CosineNoise3D()
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
