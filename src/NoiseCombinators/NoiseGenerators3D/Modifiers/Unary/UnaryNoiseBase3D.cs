namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a base class for unary noise modifiers.
/// </summary>
public abstract class UnaryNoiseBase3D : NoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryNoiseBase3D"/> class.
    /// </summary>
    /// <param name="source">The noise source that is being modified.</param>
    public UnaryNoiseBase3D(INoise3D source)
        => Source = source;

    /// <inheritdoc/>
    public override double Min => Source.Min;

    /// <inheritdoc/>
    public override double Max => Source.Max;

    /// <summary>
    /// Gets the noise source that is being modified.
    /// </summary>
    public INoise3D Source { get; }
}
