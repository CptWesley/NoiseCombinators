using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// Provides a basis for other hash based noise generators.
/// </summary>
public abstract class HashBasedNoise : NoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashBasedNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public HashBasedNoise(IHashing64 hashing)
        => Hashing = hashing;

    /// <summary>
    /// Gets the hashing function.
    /// </summary>
    public IHashing64 Hashing { get; }

    /// <inheritdoc/>
    public override sealed double Min => 0;

    /// <inheritdoc/>
    public override sealed double Max => 1;

    /// <inheritdoc/>
    public override sealed int Seed => Hashing.Seed;

    /// <summary>
    /// Gets the exact value at the given coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetHashValue(int x, int y)
    {
        ulong ly = (ulong)unchecked((uint)y) << 32;
        ulong lx = unchecked((uint)x);
        ulong h = Hashing.HashU64(ly + lx);
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
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="width">The number of values to obtain in the x-axis.</param>
    /// <param name="height">The number of values to obtain in the y-axis.</param>
    /// <returns>The obtained hash values of a chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetHashValues(int x, int y, int width, int height)
    {
        double[][] values = new double[width][];
        for (int ix = 0; ix < width; ix++)
        {
            double[] col = new double[height];
            values[ix] = col;

            for (int iy = 0; iy < height; iy++)
            {
                col[iy] = GetHashValue(x + ix, y + iy);
            }
        }

        return values;
    }
}
