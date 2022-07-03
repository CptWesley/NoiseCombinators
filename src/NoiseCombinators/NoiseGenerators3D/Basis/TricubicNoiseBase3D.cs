using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators3D.Basis;

/// <summary>
/// Provides a base for tricubic interpolation based noise generators.
/// </summary>
public abstract class TricubicNoiseBase3D : HashBasedNoise3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TricubicNoiseBase3D"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public TricubicNoiseBase3D(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TricubicNoiseBase3D"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public TricubicNoiseBase3D(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TricubicNoiseBase3D"/> class.
    /// </summary>
    public TricubicNoiseBase3D()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public sealed override double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
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
            double tx = cx - icxLow;
            double[][] col0 = values[icxLow - 1];
            double[][] col1 = values[icxLow];
            double[][] col2 = values[icxLow + 1];
            double[][] col3 = values[icxLow + 2];

            for (int iy = 0; iy < stepsY; iy++)
            {
                double[] layer = new double[stepsZ];
                col[iy] = layer;

                double cy = yOff + (iy * stepSizeY);
                int icyLow = BitUtilities.FastFloor(cy);
                double ty = cy - icyLow;

                double[] l00 = col0[icyLow - 1];
                double[] l01 = col0[icyLow];
                double[] l02 = col0[icyLow + 1];
                double[] l03 = col0[icyLow + 2];

                double[] l10 = col1[icyLow - 1];
                double[] l11 = col1[icyLow];
                double[] l12 = col1[icyLow + 1];
                double[] l13 = col1[icyLow + 2];

                double[] l20 = col2[icyLow - 1];
                double[] l21 = col2[icyLow];
                double[] l22 = col2[icyLow + 1];
                double[] l23 = col2[icyLow + 2];

                double[] l30 = col3[icyLow - 1];
                double[] l31 = col3[icyLow];
                double[] l32 = col3[icyLow + 1];
                double[] l33 = col3[icyLow + 2];

                for (int iz = 0; iz < stepSizeZ; iz++)
                {
                    double cz = zOff + (iz * stepSizeZ);
                    int iczLow = BitUtilities.FastFloor(cz);
                    double tz = cz - iczLow;

                    double v000 = l00[iczLow - 1];
                    double v001 = l00[iczLow];
                    double v002 = l00[iczLow + 1];
                    double v003 = l00[iczLow + 2];
                    double v00 = Interpolate(tz, v000, v001, v002, v003);

                    double v010 = l01[iczLow - 1];
                    double v011 = l01[iczLow];
                    double v012 = l01[iczLow + 1];
                    double v013 = l01[iczLow + 2];
                    double v01 = Interpolate(tz, v010, v011, v012, v013);

                    double v020 = l02[iczLow - 1];
                    double v021 = l02[iczLow];
                    double v022 = l02[iczLow + 1];
                    double v023 = l02[iczLow + 2];
                    double v02 = Interpolate(tz, v020, v021, v022, v023);

                    double v030 = l03[iczLow - 1];
                    double v031 = l03[iczLow];
                    double v032 = l03[iczLow + 1];
                    double v033 = l03[iczLow + 2];
                    double v03 = Interpolate(tz, v030, v031, v032, v033);
                    double v0 = Interpolate(ty, v00, v01, v02, v03);

                    double v100 = l10[iczLow - 1];
                    double v101 = l10[iczLow];
                    double v102 = l10[iczLow + 1];
                    double v103 = l10[iczLow + 2];
                    double v10 = Interpolate(tz, v100, v101, v102, v103);

                    double v110 = l11[iczLow - 1];
                    double v111 = l11[iczLow];
                    double v112 = l11[iczLow + 1];
                    double v113 = l11[iczLow + 2];
                    double v11 = Interpolate(tz, v110, v111, v112, v113);

                    double v120 = l12[iczLow - 1];
                    double v121 = l12[iczLow];
                    double v122 = l12[iczLow + 1];
                    double v123 = l12[iczLow + 2];
                    double v12 = Interpolate(tz, v120, v121, v122, v123);

                    double v130 = l13[iczLow - 1];
                    double v131 = l13[iczLow];
                    double v132 = l13[iczLow + 1];
                    double v133 = l13[iczLow + 2];
                    double v13 = Interpolate(tz, v130, v131, v132, v133);
                    double v1 = Interpolate(ty, v10, v11, v12, v13);

                    double v200 = l20[iczLow - 1];
                    double v201 = l20[iczLow];
                    double v202 = l20[iczLow + 1];
                    double v203 = l20[iczLow + 2];
                    double v20 = Interpolate(tz, v200, v201, v202, v203);

                    double v210 = l21[iczLow - 1];
                    double v211 = l21[iczLow];
                    double v212 = l21[iczLow + 1];
                    double v213 = l21[iczLow + 2];
                    double v21 = Interpolate(tz, v210, v211, v212, v213);

                    double v220 = l22[iczLow - 1];
                    double v221 = l22[iczLow];
                    double v222 = l22[iczLow + 1];
                    double v223 = l22[iczLow + 2];
                    double v22 = Interpolate(tz, v220, v221, v222, v223);

                    double v230 = l23[iczLow - 1];
                    double v231 = l23[iczLow];
                    double v232 = l23[iczLow + 1];
                    double v233 = l23[iczLow + 2];
                    double v23 = Interpolate(tz, v230, v231, v232, v233);
                    double v2 = Interpolate(ty, v20, v21, v22, v23);

                    double v300 = l30[iczLow - 1];
                    double v301 = l30[iczLow];
                    double v302 = l30[iczLow + 1];
                    double v303 = l30[iczLow + 2];
                    double v30 = Interpolate(tz, v300, v301, v302, v303);

                    double v310 = l31[iczLow - 1];
                    double v311 = l31[iczLow];
                    double v312 = l31[iczLow + 1];
                    double v313 = l31[iczLow + 2];
                    double v31 = Interpolate(tz, v310, v311, v312, v313);

                    double v320 = l32[iczLow - 1];
                    double v321 = l32[iczLow];
                    double v322 = l32[iczLow + 1];
                    double v323 = l32[iczLow + 2];
                    double v32 = Interpolate(tz, v320, v321, v322, v323);

                    double v330 = l33[iczLow - 1];
                    double v331 = l33[iczLow];
                    double v332 = l33[iczLow + 1];
                    double v333 = l33[iczLow + 2];
                    double v33 = Interpolate(tz, v330, v331, v332, v333);
                    double v3 = Interpolate(ty, v30, v31, v32, v33);
                    double v = Interpolate(tx, v0, v1, v2, v3);

                    layer[iz] = v;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Interpolates a value between <paramref name="x1"/> and <paramref name="x2"/>.
    /// </summary>
    /// <param name="t">The interpolation factor in range [0, 1].</param>
    /// <param name="x0">The value before the left value.</param>
    /// <param name="x1">The left value.</param>
    /// <param name="x2">The right value.</param>
    /// <param name="x3">The value after the right value.</param>
    /// <returns>The interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract double Interpolate(double t, double x0, double x1, double x2, double x3);
}
