using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// Provides a base for bicubic interpolation based noise generators.
/// </summary>
public abstract class BicubicNoiseBase : HashBasedNoise
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoiseBase"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BicubicNoiseBase(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoiseBase"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BicubicNoiseBase(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BicubicNoiseBase"/> class.
    /// </summary>
    public BicubicNoiseBase()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        int xLow = BitUtilities.FastFloor(x) - 1;
        int yLow = BitUtilities.FastFloor(y) - 1;
        int xHigh = BitUtilities.FastCeiling(x + ((stepsX - 1) * stepSizeX)) + 1;
        int yHigh = BitUtilities.FastCeiling(y + ((stepsY - 1) * stepSizeY)) + 1;
        int width = xHigh - xLow + 1;
        int height = yHigh - yLow + 1;
        double xOff = x - xLow;
        double yOff = y - yLow;

        double[][] values = GetHashValues(xLow, yLow, width, height);
        double[][] result = new double[stepsX][];

        for (int ix = 0; ix < stepsX; ix++)
        {
            double[] col = new double[stepsY];
            result[ix] = col;

            double cx = xOff + (ix * stepSizeX);
            int icxLow = BitUtilities.FastFloor(cx);
            double tx = cx - icxLow;
            double[] col0 = values[icxLow - 1];
            double[] col1 = values[icxLow];
            double[] col2 = values[icxLow + 1];
            double[] col3 = values[icxLow + 2];

            for (int iy = 0; iy < stepsY; iy++)
            {
                double cy = yOff + (iy * stepSizeY);
                int icyLow = BitUtilities.FastFloor(cy);
                double ty = cy - icyLow;

                double v00 = col0[icyLow - 1];
                double v01 = col1[icyLow - 1];
                double v02 = col2[icyLow - 1];
                double v03 = col3[icyLow - 1];

                double v10 = col0[icyLow];
                double v11 = col1[icyLow];
                double v12 = col2[icyLow];
                double v13 = col3[icyLow];

                double v20 = col0[icyLow + 1];
                double v21 = col1[icyLow + 1];
                double v22 = col2[icyLow + 1];
                double v23 = col3[icyLow + 1];

                double v30 = col0[icyLow + 2];
                double v31 = col1[icyLow + 2];
                double v32 = col2[icyLow + 2];
                double v33 = col3[icyLow + 2];

                double r0 = Interpolate(ty, v00, v10, v20, v30);
                double r1 = Interpolate(ty, v01, v11, v21, v31);
                double r2 = Interpolate(ty, v02, v12, v22, v32);
                double r3 = Interpolate(ty, v03, v13, v23, v33);

                col[iy] = Interpolate(tx, r0, r1, r2, r3);
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
