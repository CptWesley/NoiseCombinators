using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Modifies the seed used for generating noise.
/// </summary>
public sealed class SeededLambdaNoise3D : UnaryNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeededLambdaNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="lambda">The seed modification function.</param>
    public SeededLambdaNoise3D(INoise3D source, Func<int, int> lambda)
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
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeed(Lambda(seed), x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => throw new InvalidOperationException($"Attempted to alter seed which was not provided before. Consider using '.WithSeed(..)' with a fixed seed on this noise generator or on a noise generator containing this noise generator.");

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeedAsync(Lambda(seed), x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => throw new InvalidOperationException($"Attempted to alter seed which was not provided before. Consider using '.WithSeed(..)' with a fixed seed on this noise generator or on a noise generator containing this noise generator.");
}
