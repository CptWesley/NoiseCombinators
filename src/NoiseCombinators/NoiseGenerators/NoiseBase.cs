using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// Provides a base class for noise generators which provides some basic implementations.
/// </summary>
public abstract class NoiseBase : INoise
{
    /// <inheritdoc/>
    public abstract double Min { get; }

    /// <inheritdoc/>
    public abstract double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual double Get(double x, double y)
        => GetChunk(x, y, 1, 1)[0][0];

    /// <inheritdoc/>
    public abstract double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunk(x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunk(double x, double y, int stepsX, int stepsY)
        => GetChunk(x, y, stepsX, stepsY, 1);
}
