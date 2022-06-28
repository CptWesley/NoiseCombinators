using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a noise generator which has shifted inputs.
/// </summary>
public sealed class ShiftedNoise : UnaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShiftedNoise"/> class.
    /// </summary>
    /// <param name="source">The noise generator to invert.</param>
    /// <param name="shiftX">The amount of shift on the x-axis.</param>
    /// <param name="shiftY">The amount of shift on the y-axis.</param>
    public ShiftedNoise(INoise source, double shiftX, double shiftY)
        : base(source)
    {
        ShiftX = shiftX;
        ShiftY = shiftY;
    }

    /// <summary>
    /// Gets the amount of shift on the x-axis.
    /// </summary>
    public double ShiftX { get; }

    /// <summary>
    /// Gets the amount of shift on the y-axis.
    /// </summary>
    public double ShiftY { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeed(seed, x + ShiftX, y + ShiftY, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunk(x + ShiftX, y + ShiftY, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkWithSeedAsync(seed, x + ShiftX, y + ShiftY, stepsX, stepsY, stepSizeX, stepSizeY);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
        => Source.GetChunkAsync(x + ShiftX, y + ShiftY, stepsX, stepsY, stepSizeX, stepSizeY);
}
