using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Modifies the seed used for generating noise.
/// </summary>
public sealed class SeededNoise3D : UnaryNoiseBase3D, ISeeded
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeededNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="seed">The seed.</param>
    public SeededNoise3D(INoise3D source, int seed)
        : base(source)
    {
        Seed = seed;
    }

    /// <inheritdoc/>
    public int Seed { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeed(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeed(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeedAsync(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeedAsync(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
}
