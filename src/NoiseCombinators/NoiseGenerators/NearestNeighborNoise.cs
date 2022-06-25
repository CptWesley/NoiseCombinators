﻿using System;
using NoiseCombinators.Hashing;
using NoiseCombinators.Internal;

namespace NoiseCombinators.NoiseGenerators;

/// <summary>
/// A noise function which uses the nearest neighbor.
/// </summary>
public sealed class NearestNeighborNoise : HashBasedNoise
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise"/> class.
    /// </summary>
    /// <param name="hashing">The hashing function.</param>
    public NearestNeighborNoise(IHashing64 hashing)
        : base(hashing)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise"/> class.
    /// </summary>
    /// <param name="seed">The seed of the noise.</param>
    public NearestNeighborNoise(int seed)
        : base(new XXHash(seed))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NearestNeighborNoise"/> class.
    /// </summary>
    public NearestNeighborNoise()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public override double Get(double x, double y)
    {
        int xLow = BitUtilities.FastFloor(x);
        int yLow = BitUtilities.FastFloor(y);
        return GetHashValue(xLow, yLow);
    }

    /// <inheritdoc/>
    public override double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        int iStepsX = BitUtilities.FastCeiling(stepsX * stepSizeX);
        int iStepsY = BitUtilities.FastCeiling(stepsY * stepSizeY);

        int xLow = BitUtilities.FastFloor(x);
        int yLow = BitUtilities.FastFloor(y);
        double xOff = x - xLow;
        double yOff = y - yLow;

        double[][] values = GetHashValues(xLow, yLow, iStepsX, iStepsY);
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
