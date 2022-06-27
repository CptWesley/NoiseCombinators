using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a combinator which subtracts a scalar from another noise generator.
/// </summary>
public sealed class SubtractedScalarNoise : UnaryValueMapNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubtractedScalarNoise"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="scalar">The scalar to add.</param>
    public SubtractedScalarNoise(INoise source, double scalar)
        : base(source)
    {
        Min = source.Min - scalar;
        Max = source.Max - scalar;
        Scalar = scalar;
    }

    /// <summary>
    /// Gets the scalar subtracted from every value.
    /// </summary>
    public double Scalar { get; }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => value - Scalar;
}
