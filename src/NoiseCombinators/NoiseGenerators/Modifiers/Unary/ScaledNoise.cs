using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a scaled view of a given source noise generator.
/// </summary>
public sealed class ScaledNoise : UnaryNoiseBase
{
    private readonly double scaleXInv;
    private readonly double scaleYInv;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScaledNoise"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="scaleX">The scale to scale with on the x-axis.</param>
    /// <param name="scaleY">The scale to scale with on the y-axis.</param>
    public ScaledNoise(INoise source, double scaleX, double scaleY)
        : base(source)
    {
        ScaleX = scaleX;
        ScaleY = scaleY;
        scaleXInv = 1d / scaleX;
        scaleYInv = 1d / scaleY;
    }

    /// <summary>
    /// Gets the scale used when sampling the source generator on the x-axis.
    /// </summary>
    public double ScaleX { get; }

    /// <summary>
    /// Gets the scale used when sampling the source generator on the y-axis.
    /// </summary>
    public double ScaleY { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeed(seed, x * scaleXInv, y * scaleYInv, stepsX, stepsY, stepSizeX * scaleXInv, stepSizeY * scaleYInv);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunk(x * scaleXInv, y * scaleYInv, stepsX, stepsY, stepSizeX * scaleXInv, stepSizeY * scaleYInv);
}
