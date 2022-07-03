using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Binary;

/// <summary>
/// Provides a combinator which subtracts the results of two noise generators.
/// </summary>
public sealed class SubtractedNoise2D : BinaryValueMapNoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubtractedNoise2D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public SubtractedNoise2D(INoise2D sourceLeft, INoise2D sourceRight)
        : base(sourceLeft, sourceRight)
    {
        Min = sourceLeft.Min - sourceRight.Max;
        Max = sourceLeft.Max - sourceRight.Min;
    }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double valueLeft, double valueRight)
        => valueLeft - valueRight;
}
