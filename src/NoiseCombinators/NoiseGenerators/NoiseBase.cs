using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
    public abstract double[][] GetChunkWithSeed2D(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunkWithSeed2D(int seed, double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunkWithSeed2D(seed, x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunkWithSeed2D(int seed, double x, double y, int stepsX, int stepsY)
        => GetChunkWithSeed2D(seed, x, y, stepsX, stepsY, 1);

    /// <inheritdoc/>
    public abstract double[][] GetChunk2D(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunk2D(double x, double y, int stepsX, int stepsY, double stepSize)
        => GetChunk2D(x, y, stepsX, stepsY, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetChunk2D(double x, double y, int stepsX, int stepsY)
        => GetChunk2D(x, y, stepsX, stepsY, 1);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][]> GetChunkWithSeed2DAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkWithSeed2DAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSize)
        => Task.Run(() => GetChunkWithSeed2D(seed, x, y, stepsX, stepsY, stepSize));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunkWithSeed2DAsync(int seed, double x, double y, int stepsX, int stepsY)
        => Task.Run(() => GetChunkWithSeed2D(seed, x, y, stepsX, stepsY));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][]> GetChunk2DAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunk2DAsync(double x, double y, int stepsX, int stepsY, double stepSize)
        => Task.Run(() => GetChunk2D(x, y, stepsX, stepsY, stepSize));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][]> GetChunk2DAsync(double x, double y, int stepsX, int stepsY)
        => Task.Run(() => GetChunk2D(x, y, stepsX, stepsY));
}
