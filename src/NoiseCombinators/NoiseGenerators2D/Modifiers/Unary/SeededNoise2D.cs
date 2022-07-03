using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Unary;

/// <summary>
/// Modifies the seed used for generating noise.
/// </summary>
public sealed class SeededNoise2D : UnaryNoiseBase2D, ISeeded
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeededNoise2D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="seed">The seed.</param>
    public SeededNoise2D(INoise2D source, int seed)
        : base(source)
    {
        Seed = seed;
    }

    /// <inheritdoc/>
    public int Seed { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeed(Seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeed(Seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeedAsync(Seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeedAsync(Seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);
}
