using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators2D;

/// <summary>
/// Provides a base class for noise generators which provides some basic implementations.
/// </summary>
public abstract class NoiseBase2D : INoise2D
{
    /// <inheritdoc/>
    public abstract double Min { get; }

    /// <inheritdoc/>
    public abstract double Max { get; }

    /// <inheritdoc/>
    public abstract double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunkWithSeed(seed, x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY)
        => GetChunkWithSeed(seed, x, y, stepsX, stepsY, 1);

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

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunkWithSeedAsync(seed, x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY)
        => GetChunkWithSeedAsync(seed, x, y, stepsX, stepsY, 1);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunkAsync(x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY)
        => GetChunkAsync(x, y, stepsX, stepsY, 1);
}
