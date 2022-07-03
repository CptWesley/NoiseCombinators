using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a basis for noise modifiers that remap values.
/// </summary>
public abstract class UnaryValueMapNoiseBase3D : UnaryNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryValueMapNoiseBase3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to modify.</param>
    protected UnaryValueMapNoiseBase3D(INoise3D source)
        : base(source)
    {
    }

    /// <inheritdoc/>
    public override abstract double Min { get; }

    /// <inheritdoc/>
    public override abstract double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result = Source.GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        CombineResults(result, stepsX, stepsY, stepsZ);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result = Source.GetChunk(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        CombineResults(result, stepsX, stepsY, stepsZ);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result = await Source.GetChunkWithSeedAsync(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ).ConfigureAwait(false);
        CombineResults(result, stepsX, stepsY, stepsZ);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result = await Source.GetChunkAsync(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ).ConfigureAwait(false);
        CombineResults(result, stepsX, stepsY, stepsZ);
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
    private void CombineResults(double[][][] result, int width, int height, int depth)
    {
        for (int ix = 0; ix < width; ix++)
        {
            double[][] col = result[ix];
            for (int iy = 0; iy < height; iy++)
            {
                double[] layer = col[iy];

                for (int iz = 0; iz < depth; iz++)
                {
                    layer[iy] = MapValue(layer[iy]);
                }
            }
        }
    }
}
