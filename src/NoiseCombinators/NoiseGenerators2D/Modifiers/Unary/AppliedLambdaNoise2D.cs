using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Unary;

/// <summary>
/// Provides a combinator which applies a function to all values produced by the noise generator.
/// </summary>
public sealed class AppliedLambdaNoise2D : UnaryValueMapNoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppliedLambdaNoise2D"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="lambda">The function to apply.</param>
    /// <param name="minModulator">The function to compute the minimum value of this noise generator.</param>
    /// <param name="maxModulator">The function to compute the maximum value of this noise generator.</param>
    public AppliedLambdaNoise2D(INoise2D source, Func<double, double> lambda, Func<INoise2D, double> minModulator, Func<INoise2D, double> maxModulator)
        : base(source)
    {
        Min = minModulator(source);
        Max = maxModulator(source);
        Lambda = lambda;
    }

    /// <summary>
    /// Gets the function to apply to each value.
    /// </summary>
    public Func<double, double> Lambda { get; }

    /// <inheritdoc/>
    public override double Min { get; }

    /// <inheritdoc/>
    public override double Max { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override double MapValue(double value)
        => Lambda(value);
}
