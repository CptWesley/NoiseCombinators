namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a base class for unary noise modifiers.
/// </summary>
public abstract class UnaryNoiseBase : NoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryNoiseBase"/> class.
    /// </summary>
    /// <param name="source">The noise source that is being modified.</param>
    public UnaryNoiseBase(INoise source)
        => Source = source;

    /// <inheritdoc/>
    public override double Min => Source.Min;

    /// <inheritdoc/>
    public override double Max => Source.Max;

    /// <summary>
    /// Gets the noise source that is being modified.
    /// </summary>
    public INoise Source { get; }
}
