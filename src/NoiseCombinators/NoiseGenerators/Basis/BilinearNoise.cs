using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators.Basis;

/// <summary>
/// Provides a noise generator which applies bilinear interpolation to white noise.
/// </summary>
public sealed class BilinearNoise : BilinearNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BilinearNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BilinearNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoise"/> class.
    /// </summary>
    public BilinearNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1)
        => (x0 * (1 - t)) + (x1 * t);
}
