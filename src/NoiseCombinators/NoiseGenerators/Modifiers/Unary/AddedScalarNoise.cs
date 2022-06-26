using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a combinator which adds a scalar to another noise generator.
/// </summary>
public sealed class AddedScalarNoise : UnaryValueMapNoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddedScalarNoise"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="scalar">The scalar to add.</param>
    public AddedScalarNoise(INoise source, double scalar)
        : base(source)
    {
        Min = source.Min + scalar;
        Max = source.Max + scalar;
    }

    /// <summary>
    /// Gets the scalar added to every value.
    /// </summary>
    public double Scalar { get; }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => value + Scalar;
}
