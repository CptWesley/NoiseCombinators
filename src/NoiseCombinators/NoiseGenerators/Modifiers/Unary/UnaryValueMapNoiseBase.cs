using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a basis for noise modifiers that remap values.
/// </summary>
public abstract class UnaryValueMapNoiseBase : UnaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryValueMapNoiseBase"/> class.
    /// </summary>
    /// <param name="source">The noise generator to modify.</param>
    protected UnaryValueMapNoiseBase(INoise source)
        : base(source)
    {
    }

    /// <inheritdoc/>
    public override abstract double Min { get; }

    /// <inheritdoc/>
    public override abstract double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunkWithSeed2D(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result = Source.GetChunkWithSeed2D(seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        CombineResults(result, stepsX, stepsY);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][] GetChunk2D(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result = Source.GetChunk2D(x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        CombineResults(result, stepsX, stepsY);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][]> GetChunkWithSeed2DAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result = await Source.GetChunkWithSeed2DAsync(seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY).ConfigureAwait(false);
        CombineResults(result, stepsX, stepsY);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][]> GetChunk2DAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result = await Source.GetChunk2DAsync(x, y, stepsX, stepsY, stepSizeX, stepSizeY).ConfigureAwait(false);
        CombineResults(result, stepsX, stepsY);
        return result;
    }

    /// <summary>
    /// Modify a value from the source noise.
    /// </summary>
    /// <param name="value">The value from the source noise.</param>
    /// <returns>The modified value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract double MapValue(double value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CombineResults(double[][] result, int width, int height)
    {
        for (int ix = 0; ix < width; ix++)
        {
            double[] col = result[ix];
            for (int iy = 0; iy < height; iy++)
            {
                col[iy] = MapValue(col[iy]);
            }
        }
    }
}
