using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a basis for other hash based noise generators.
/// </summary>
public abstract class HashBasedNoise3D : NoiseBase3D, ISeeded
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashBasedNoise3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public HashBasedNoise3D(IHashing64 hashing)
        => Hashing = hashing;

    /// <summary>
    /// Gets the hashing function.
    /// </summary>
    public IHashing64 Hashing { get; }

    /// <inheritdoc/>
    public override double Min => 0;

    /// <inheritdoc/>
    public override double Max => 1;

    /// <inheritdoc/>
    public int Seed => Hashing.Seed;

    /// <summary>
    /// Gets the exact value at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <param name="z">The z-coordinate.</param>
    /// <returns>The value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetHashValue(int seed, int x, int y, int z)
    {
        ulong ly = (ulong)unchecked((uint)y) << 32;
        ulong lx = unchecked((uint)x);
        ulong lz = unchecked((uint)z);

        ulong h = Hashing.HashU64WithSeed(seed, lx + ly, lz);
        double result = h / (double)ulong.MaxValue;

        if (result < 0)
        {
            return 0;
        }

        if (result > 1)
        {
            return 1;
        }

        return result;
    }

    /// <summary>
    /// Gets the hash values of a chunk for the inner hash function.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="z">The starting z-coordinate.</param>
    /// <param name="width">The number of values to obtain in the x-axis.</param>
    /// <param name="height">The number of values to obtain in the y-axis.</param>
    /// <param name="depth">The number of values to obtain in the z-axis.</param>
    /// <returns>The obtained hash values of a chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][][] GetHashValues(int seed, int x, int y, int z, int width, int height, int depth)
    {
        double[][][] values = new double[width][][];
        for (int ix = 0; ix < width; ix++)
        {
            double[][] col = new double[height][];
            values[ix] = col;

            for (int iy = 0; iy < height; iy++)
            {
                double[] layer = new double[depth];
                col[iy] = layer;

                for (int iz = 0; iz < depth; iz++)
                {
                    layer[iz] = GetHashValue(seed, x + ix, y + iy, z + iz);
                }
            }
        }

        return values;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => GetChunkWithSeed(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => GetChunkWithSeedAsync(Seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Task.Run(() => GetChunkWithSeed(seed, x, y, z, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ));
}
