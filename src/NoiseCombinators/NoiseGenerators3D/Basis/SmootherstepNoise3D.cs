using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a noise generator which applies three dimensional smootherstep interpolation to generate noise.
/// </summary>
public sealed class SmootherstepNoise3D : TrilinearNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public SmootherstepNoise3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public SmootherstepNoise3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmootherstepNoise3D"/> class.
    /// </summary>
    public SmootherstepNoise3D()
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
