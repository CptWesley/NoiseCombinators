using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Binary;

/// <summary>
/// Provides a combinator which multiplies the results of two noise generators.
/// </summary>
public sealed class MultipliedNoise2D : BinaryValueMapNoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultipliedNoise2D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public MultipliedNoise2D(INoise2D sourceLeft, INoise2D sourceRight)
        : base(sourceLeft, sourceRight)
    {
        double mimi = sourceLeft.Min * sourceRight.Min;
        double mima = sourceLeft.Min * sourceRight.Max;
        double mami = sourceLeft.Max * sourceRight.Min;
        double mama = sourceLeft.Max * sourceRight.Max;

        Min = Math.Min(mimi, Math.Min(mima, Math.Min(mami, mama)));
        Max = Math.Max(mimi, Math.Max(mima, Math.Max(mami, mama)));
    }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double valueLeft, double valueRight)
        => valueLeft * valueRight;
}
