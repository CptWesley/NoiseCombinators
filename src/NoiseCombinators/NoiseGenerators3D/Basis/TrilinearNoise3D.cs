using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a noise generator which applies trilinear interpolation to white noise.
/// </summary>
public sealed class TrilinearNoise3D : TrilinearNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoise3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public TrilinearNoise3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoise3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public TrilinearNoise3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoise3D"/> class.
    /// </summary>
    public TrilinearNoise3D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double Interpolate(double t, double x0, double x1)
        => (x0 * (1 - t)) + (x1 * t);
}
