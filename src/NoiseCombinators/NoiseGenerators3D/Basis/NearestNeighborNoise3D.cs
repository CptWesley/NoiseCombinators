using System;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// A noise function which uses the nearest neighbor.
/// </summary>
public sealed class NearestNeighborNoise3D : HashBasedNoise3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public NearestNeighborNoise3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public NearestNeighborNoise3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise3D"/> class.
    /// </summary>
    public NearestNeighborNoise3D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        int iStepsX = BitUtilities.FastCeiling(stepsX * stepSizeX) + 1;
        int iStepsY = BitUtilities.FastCeiling(stepsY * stepSizeY) + 1;
        int iStepsZ = BitUtilities.FastCeiling(stepsZ * stepSizeZ) + 1;

        int xLow = BitUtilities.FastFloor(x);
        int yLow = BitUtilities.FastFloor(y);
        int zLow = BitUtilities.FastFloor(z);
        double xOff = x - xLow;
        double yOff = y - yLow;
        double zOff = z - zLow;

        double[][][] values = GetHashValues(seed, xLow, yLow, zLow, iStepsX, iStepsY, iStepsZ);
        double[][][] result = new double[stepsX][][];

        for (int ix = 0; ix < stepsX; ix++)
        {
            double[][] col = new double[stepsY][];
            result[ix] = col;

            int icx = BitUtilities.FastFloor(xOff + (ix * stepSizeX));
            double[][] colValues = values[icx];

            for (int iy = 0; iy < stepsY; iy++)
            {
                double[] layer = new double[stepsY];
                col[iy] = layer;

                int icy = BitUtilities.FastFloor(yOff + (iy * stepSizeY));

                double[] layerValues = colValues[iy];

                for (int iz = 0; iz < stepsZ; iz++)
                {
                    layer[iz] = layerValues[iz];
                }
            }
        }

        return result;
    }
}
