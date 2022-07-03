using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a noise generator which inverts the values within the original range.
/// </summary>
public sealed class InvertedNoise3D : UnaryValueMapNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvertedNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to invert.</param>
    public InvertedNoise3D(INoise3D source)
        : base(source)
    {
    }

    /// <inheritdoc/>
    public override sealed double Min => Source.Min;

    /// <inheritdoc/>
    public override sealed double Max => Source.Max;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => Min + Max - value;
}
