using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Unary;

/// <summary>
/// Provides a combinator which subtracts a scalar from another noise generator.
/// </summary>
public sealed class SubtractedScalarNoise2D : UnaryValueMapNoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubtractedScalarNoise2D"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="scalar">The scalar to add.</param>
    public SubtractedScalarNoise2D(INoise2D source, double scalar)
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
