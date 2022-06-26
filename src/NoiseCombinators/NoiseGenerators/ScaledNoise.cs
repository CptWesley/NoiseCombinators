using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// Provides a scaled view of a given source noise generator.
/// </summary>
public sealed class ScaledNoise : UnaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScaledNoise"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="scale">The scale to scale with.</param>
    public ScaledNoise(INoise source, double scale)
        : base(source)
    {
        Scale = scale;
    }

    /// <summary>
    /// Gets the scale used when sampling the source generator.
    /// </summary>
    public double Scale { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunk(x * Scale, y * Scale, stepsX, stepsY, stepSizeX * Scale, stepSizeY * Scale);
}
