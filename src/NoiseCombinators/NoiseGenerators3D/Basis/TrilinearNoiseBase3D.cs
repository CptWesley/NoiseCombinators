using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a base for trilinear interpolation based noise generators.
/// </summary>
public abstract class TrilinearNoiseBase3D : HashBasedNoise3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoiseBase3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public TrilinearNoiseBase3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoiseBase3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public TrilinearNoiseBase3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrilinearNoiseBase3D"/> class.
    /// </summary>
    public TrilinearNoiseBase3D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
    {
        int xLow = BitUtilities.FastFloor(x) - 1;
        int yLow = BitUtilities.FastFloor(y) - 1;
        int zLow = BitUtilities.FastFloor(z) - 1;
        int xHigh = BitUtilities.FastCeiling(x + ((stepsX - 1) * stepSizeX)) + 1;
        int yHigh = BitUtilities.FastCeiling(y + ((stepsY - 1) * stepSizeY)) + 1;
        int zHigh = BitUtilities.FastCeiling(y + ((stepsZ - 1) * stepSizeZ)) + 1;
        int width = xHigh - xLow + 1;
        int height = yHigh - yLow + 1;
        int depth = zHigh - zLow + 1;
        double xOff = x - xLow;
        double yOff = y - yLow;
        double zOff = z - zLow;

        double[][][] values = GetHashValues(seed, xLow, yLow, zLow, width, height, depth);
        double[][][] result = new double[stepsX][][];

        for (int ix = 0; ix < stepsX; ix++)
        {
            double[][] col = new double[stepsY][];
            result[ix] = col;

            double cx = xOff + (ix * stepSizeX);
            int icxLow = BitUtilities.FastFloor(cx);
            int icxHigh = icxLow + 1;
            double tx = cx - icxLow;
            double[][] v0 = values[icxLow];
            double[][] v1 = values[icxHigh];

            for (int iy = 0; iy < stepsY; iy++)
            {
                double[] layer = new double[stepsZ];
                col[iy] = layer;

                double cy = yOff + (iy * stepSizeY);
                int icyLow = BitUtilities.FastFloor(cy);
                int icyHigh = icyLow + 1;
                double ty = cy - icyLow;

                double[] v00 = v0[icyLow];
                double[] v01 = v0[icyHigh];
                double[] v10 = v1[icyLow];
                double[] v11 = v1[icyHigh];

                for (int iz = 0; iz < stepsZ; iz++)
                {
                    double cz = zOff + (iz * stepSizeZ);
                    int iczLow = BitUtilities.FastFloor(cz);
                    int iczHigh = iczLow + 1;
                    double tz = cz - iczLow;

                    double v000 = v00[iczLow];
                    double v001 = v00[iczHigh];
                    double r00 = Interpolate(tz, v000, v001);

                    double v010 = v01[iczLow];
                    double v011 = v01[iczHigh];
                    double r01 = Interpolate(tz, v010, v011);
                    double r0 = Interpolate(ty, r00, r01);

                    double v100 = v10[iczLow];
                    double v101 = v10[iczHigh];
                    double r10 = Interpolate(tz, v100, v101);

                    double v110 = v11[iczLow];
                    double v111 = v11[iczHigh];
                    double r11 = Interpolate(tz, v110, v111);
                    double r1 = Interpolate(ty, r10, r11);

                    double r = Interpolate(tx, r0, r1);
                    layer[iz] = r;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Interpolates between the two given values.
    /// </summary>
    /// <param name="t">The interpolation factor in range [0, 1].</param>
    /// <param name="x0">The left value.</param>
    /// <param name="x1">The right value.</param>
    /// <returns>The interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract double Interpolate(double t, double x0, double x1);
}
