using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a combinator which applies a function to all values produced by the noise generator.
/// </summary>
public sealed class AppliedLambdaNoise3D : UnaryValueMapNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppliedLambdaNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise source.</param>
    /// <param name="lambda">The function to apply.</param>
    /// <param name="minModulator">The function to compute the minimum value of this noise generator.</param>
    /// <param name="maxModulator">The function to compute the maximum value of this noise generator.</param>
    public AppliedLambdaNoise3D(INoise3D source, Func<double, double> lambda, Func<INoise3D, double> minModulator, Func<INoise3D, double> maxModulator)
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
