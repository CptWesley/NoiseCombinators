using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Modifies the seed used for generating noise.
/// </summary>
public sealed class SeededNoise : UnaryNoiseBase, ISeeded
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeededNoise"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="seed">The seed.</param>
    public SeededNoise(INoise source, int seed)
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
}
