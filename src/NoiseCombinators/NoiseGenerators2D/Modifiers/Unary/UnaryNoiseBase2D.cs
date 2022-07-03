namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Unary;

/// <summary>
/// Provides a base class for unary noise modifiers.
/// </summary>
public abstract class UnaryNoiseBase2D : NoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryNoiseBase2D"/> class.
    /// </summary>
    /// <param name="source">The noise source that is being modified.</param>
    public UnaryNoiseBase2D(INoise2D source)
        => Source = source;

    /// <inheritdoc/>
    public override double Min => Source.Min;

    /// <inheritdoc/>
    public override double Max => Source.Max;

    /// <summary>
    /// Gets the noise source that is being modified.
    /// </summary>
    public INoise2D Source { get; }
}
