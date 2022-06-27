﻿using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a basis for noise modifiers that remap values.
/// </summary>
public abstract class UnaryValueMapNoiseBase : UnaryNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryValueMapNoiseBase"/> class.
    /// </summary>
    /// <param name="source">The noise generator to modify.</param>
    protected UnaryValueMapNoiseBase(INoise source)
        : base(source)
    {
    }

    /// <inheritdoc/>
    public override abstract double Min { get; }

    /// <inheritdoc/>
    public override abstract double Max { get; }

    /// <inheritdoc/>
    public override sealed double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double[][] result = Source.GetChunk(x, y, stepsX, stepsY, stepSizeX, stepSizeY);

        for (int ix = 0; ix < stepsX; ix++)
        {
            double[] col = result[ix];
            for (int iy = 0; iy < stepsY; iy++)
            {
                col[iy] = MapValue(col[iy]);
            }
        }

        return result;
    }

    /// <summary>
    /// Modify a value from the source noise.
    /// </summary>
    /// <param name="value">The value from the source noise.</param>
    /// <returns>The modified value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract double MapValue(double value);
}