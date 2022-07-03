using System;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators2D.Basis;

/// <summary>
/// A noise function which uses the nearest neighbor.
/// </summary>
public sealed class NearestNeighborNoise2D : HashBasedNoise2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise2D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public NearestNeighborNoise2D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise2D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public NearestNeighborNoise2D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise2D"/> class.
    /// </summary>
    public NearestNeighborNoise2D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        int iStepsX = BitUtilities.FastCeiling(stepsX * stepSizeX) + 1;
        int iStepsY = BitUtilities.FastCeiling(stepsY * stepSizeY) + 1;

        int xLow = BitUtilities.FastFloor(x);
        int yLow = BitUtilities.FastFloor(y);
        double xOff = x - xLow;
        double yOff = y - yLow;

        double[][] values = GetHashValues(seed, xLow, yLow, iStepsX, iStepsY);
        double[][] result = new double[stepsX][];

        for (int ix = 0; ix < stepsX; ix++)
        {
            double[] col = new double[stepsY];
            result[ix] = col;

            int icx = BitUtilities.FastFloor(xOff + (ix * stepSizeX));
            double[] colValues = values[icx];

            for (int iy = 0; iy < stepsY; iy++)
            {
                int icy = BitUtilities.FastFloor(yOff + (iy * stepSizeY));
                col[iy] = colValues[icy];
            }
        }

        return result;
    }
}
