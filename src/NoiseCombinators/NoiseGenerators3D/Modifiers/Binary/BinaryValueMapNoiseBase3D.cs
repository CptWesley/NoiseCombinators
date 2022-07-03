using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Binary;

/// <summary>
/// Provides a basis for noise modifiers that remap values.
/// </summary>
public abstract class BinaryValueMapNoiseBase3D : BinaryNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryValueMapNoiseBase3D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public BinaryValueMapNoiseBase3D(INoise3D sourceLeft, INoise3D sourceRight)
        : base(sourceLeft, sourceRight)
    {
    }

    /// <inheritdoc/>
    public override abstract double Min { get; }

    /// <inheritdoc/>
    public override abstract double Max { get; }

    /// <inheritdoc/>
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result1 = SourceLeft.GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        double[][][] result2 = SourceRight.GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        CombineResults(result1, result2, stepsX, stepsY, stepsZ);
        return result1;
    }

    /// <inheritdoc/>
    public override sealed double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        double[][][] result1 = SourceLeft.GetChunk(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        double[][][] result2 = SourceRight.GetChunk(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        CombineResults(result1, result2, stepsX, stepsY, stepsZ);
        return result1;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        Task<double[][][]> t1 = SourceLeft.GetChunkWithSeedAsync(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        Task<double[][][]> t2 = SourceRight.GetChunkWithSeedAsync(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        double[][][] result1 = await t1.ConfigureAwait(false);
        double[][][] result2 = await t2.ConfigureAwait(false);
        CombineResults(result1, result2, stepsX, stepsY, stepsZ);
        return result1;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed async Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        Task<double[][][]> t1 = SourceLeft.GetChunkAsync(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        Task<double[][][]> t2 = SourceRight.GetChunkAsync(x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
        double[][][] result1 = await t1.ConfigureAwait(false);
        double[][][] result2 = await t2.ConfigureAwait(false);
        CombineResults(result1, result2, stepsX, stepsY, stepsZ);
        return result1;
    }

    /// <summary>
    /// Modify a value from the source noise.
    /// </summary>
    /// <param name="valueLeft">The value from the left source noise.</param>
    /// <param name="valueRight">The value from the right source noise.</param>
    /// <returns>The modified value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract double MapValue(double valueLeft, double valueRight);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CombineResults(double[][][] result1, double[][][] result2, int width, int height, int depth)
    {
        for (int ix = 0; ix < width; ix++)
        {
            double[][] col1 = result1[ix];
            double[][] col2 = result2[ix];
            for (int iy = 0; iy < height; iy++)
            {
                double[] layer1 = col1[iy];
                double[] layer2 = col2[iy];

                for (int iz = 0; iz < depth; iz++)
                {
                    layer1[iz] = MapValue(layer1[iz], layer2[iz]);
                }
            }
        }
    }
}
