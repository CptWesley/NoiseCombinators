namespace NoiseCombinators.NoiseGenerators.Modifiers.Binary;

/// <summary>
/// Provides a basis for binary operations on noise generators.
/// </summary>
public abstract class BinaryNoiseBase : NoiseBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryNoiseBase"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public BinaryNoiseBase(INoise sourceLeft, INoise sourceRight)
        => (SourceLeft, SourceRight) = (sourceLeft, sourceRight);

    /// <summary>
    /// Gets the first noise source that is being modified.
    /// </summary>
    public INoise SourceLeft { get; }

    /// <summary>
    /// Gets the second noise source that is being modified.
    /// </summary>
    public INoise SourceRight { get; }
}
