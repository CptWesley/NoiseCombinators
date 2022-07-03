using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators2D.Basis;

/// <summary>
/// Provides a noise generator which applies bilinear interpolation to white noise.
/// </summary>
public sealed class BilinearNoise2D : BilinearNoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise2D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BilinearNoise2D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise2D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BilinearNoise2D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise2D"/> class.
    /// </summary>
    public BilinearNoise2D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1)
        => (x0 * (1 - t)) + (x1 * t);
}
