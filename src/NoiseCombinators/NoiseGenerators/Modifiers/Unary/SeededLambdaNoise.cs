using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Modifies the seed used for generating noise.
/// </summary>
public sealed class SeededLambdaNoise : UnaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeededLambdaNoise"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="lambda">The seed modification function.</param>
    public SeededLambdaNoise(INoise source, Func<int, int> lambda)
        : base(source)
    {
        Lambda = lambda;
    }

    /// <summary>
    /// Gets the function which modifies the seed value.
    /// </summary>
    public Func<int, int> Lambda { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeed(Lambda(seed), x, y, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => throw new InvalidOperationException($"Attempted to alter seed which was not provided before. Consider using '.WithSeed(..)' with a fixed seed on this noise generator or on a noise generator containing this noise generator.");

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeedAsync(Lambda(seed), x, y, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => throw new InvalidOperationException($"Attempted to alter seed which was not provided before. Consider using '.WithSeed(..)' with a fixed seed on this noise generator or on a noise generator containing this noise generator.");
}
