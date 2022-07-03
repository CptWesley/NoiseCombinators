using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Binary;

/// <summary>
/// Provides a combinator which subtracts the results of two noise generators.
/// </summary>
public sealed class SubtractedNoise3D : BinaryValueMapNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubtractedNoise3D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public SubtractedNoise3D(INoise3D sourceLeft, INoise3D sourceRight)
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
