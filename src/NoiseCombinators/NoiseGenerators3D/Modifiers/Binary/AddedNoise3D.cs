using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Binary;

/// <summary>
/// Provides a combinator which adds the results of two noise generators.
/// </summary>
public sealed class AddedNoise3D : BinaryValueMapNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddedNoise3D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public AddedNoise3D(INoise3D sourceLeft, INoise3D sourceRight)
        : base(sourceLeft, sourceRight)
    {
        Min = sourceLeft.Min + sourceRight.Min;
        Max = sourceLeft.Max + sourceRight.Max;
    }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double valueLeft, double valueRight)
        => valueLeft + valueRight;
}
