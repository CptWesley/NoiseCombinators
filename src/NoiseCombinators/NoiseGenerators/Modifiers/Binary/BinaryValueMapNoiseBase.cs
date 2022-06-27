﻿using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Binary;

/// <summary>
/// Provides a basis for noise modifiers that remap values.
/// </summary>
public abstract class BinaryValueMapNoiseBase : BinaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryValueMapNoiseBase"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public BinaryValueMapNoiseBase(INoise sourceLeft, INoise sourceRight)
        : base(sourceLeft, sourceRight)
    {
    }

    /// <inheritdoc/>
    public override abstract double Min { get; }

    /// <inheritdoc/>
    public override abstract double Max { get; }

    /// <inheritdoc/>
    public override sealed double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result1 = SourceLeft.GetChunkWithSeed(seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        double[][] result2 = SourceRight.GetChunkWithSeed(seed, x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        CombineResults(result1, result2, stepsX, stepsY);
        return result1;
    }

    /// <inheritdoc/>
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result1 = SourceLeft.GetChunk(x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        double[][] result2 = SourceRight.GetChunk(x, y, stepsX, stepsY, stepSizeX, stepSizeY);
        CombineResults(result1, result2, stepsX, stepsY);
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
    private void CombineResults(double[][] result1, double[][] result2, int width, int height)
    {
        for (int ix = 0; ix < width; ix++)
        {
            double[] col1 = result1[ix];
            double[] col2 = result2[ix];
            for (int iy = 0; iy < height; iy++)
            {
                col1[iy] = MapValue(col1[iy], col2[iy]);
            }
        }
    }
}
