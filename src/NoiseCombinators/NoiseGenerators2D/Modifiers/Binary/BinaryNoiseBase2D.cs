namespace NoiseCombinators.NoiseGenerators2D.Modifiers.Binary;

/// <summary>
/// Provides a basis for binary operations on noise generators.
/// </summary>
public abstract class BinaryNoiseBase2D : NoiseBase2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryNoiseBase2D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public BinaryNoiseBase2D(INoise2D sourceLeft, INoise2D sourceRight)
        => (SourceLeft, SourceRight) = (sourceLeft, sourceRight);

    /// <summary>
    /// Gets the first noise source that is being modified.
    /// </summary>
    public INoise2D SourceLeft { get; }

    /// <summary>
    /// Gets the second noise source that is being modified.
    /// </summary>
    public INoise2D SourceRight { get; }
}
