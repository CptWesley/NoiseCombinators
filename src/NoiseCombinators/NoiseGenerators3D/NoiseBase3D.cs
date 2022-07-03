using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D;

/// <summary>
/// Provides a base class for noise generators which provides some basic implementations.
/// </summary>
public abstract class NoiseBase3D : INoise3D
{
    /// <inheritdoc/>
    public abstract double Min { get; }

    /// <inheritdoc/>
    public abstract double Max { get; }

    /// <inheritdoc/>
    public abstract double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSize)
        => GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSize, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ)
        => GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, 1);

    /// <inheritdoc/>
    public abstract double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSize)
        => GetChunk(x, y, z, stepsX, stepsY, stepsZ, stepSize, stepSize, stepSize);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ)
        => GetChunk(x, y, z, stepsX, stepsY, stepsZ, 1);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSize)
        => Task.Run(() => GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSize));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ)
        => Task.Run(() => GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSize)
        => Task.Run(() => GetChunk(x, y, z, stepsX, stepsY, stepsZ, stepSize));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ)
        => Task.Run(() => GetChunk(x, y, z, stepsX, stepsY, stepsZ));
}
