using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators.Basis;

/// <summary>
/// Provides a base for bilinear interpolation based noise generators.
/// </summary>
public abstract class BilinearNoiseBase : HashBasedNoise
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoiseBase"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public BilinearNoiseBase(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoiseBase"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public BilinearNoiseBase(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BilinearNoiseBase"/> class.
    /// </summary>
    public BilinearNoiseBase()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        int xLow = BitUtilities.FastFloor(x);
        int yLow = BitUtilities.FastFloor(y);
        int xHigh = BitUtilities.FastCeiling(x + ((stepsX - 1) * stepSizeX));
        int yHigh = BitUtilities.FastCeiling(y + ((stepsY - 1) * stepSizeY));
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
            int icxHigh = icxLow + 1;
            double tx = cx - icxLow;
            double[] colValuesLow = values[icxLow];
            double[] colValuesHigh = values[icxHigh];

            for (int iy = 0; iy < stepsY; iy++)
            {
                double cy = yOff + (iy * stepSizeY);
                int icyLow = BitUtilities.FastFloor(cy);
                int icyHigh = icyLow + 1;
                double ty = cy - icyLow;

                double v00 = colValuesLow[icyLow];
                double v01 = colValuesHigh[icyLow];
                double v10 = colValuesLow[icyHigh];
                double v11 = colValuesHigh[icyHigh];

                double r0 = Interpolate(ty, v00, v10);
                double r1 = Interpolate(ty, v01, v11);

                col[iy] = Interpolate(tx, r0, r1);
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
