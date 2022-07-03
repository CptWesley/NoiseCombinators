namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Binary;

/// <summary>
/// Provides a basis for binary operations on noise generators.
/// </summary>
public abstract class BinaryNoiseBase3D : NoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryNoiseBase3D"/> class.
    /// </summary>
    /// <param name="sourceLeft">The first noise source.</param>
    /// <param name="sourceRight">The second noise source.</param>
    public BinaryNoiseBase3D(INoise3D sourceLeft, INoise3D sourceRight)
        => (SourceLeft, SourceRight) = (sourceLeft, sourceRight);

    /// <summary>
    /// Gets the first noise source that is being modified.
    /// </summary>
    public INoise3D SourceLeft { get; }

    /// <summary>
    /// Gets the second noise source that is being modified.
    /// </summary>
    public INoise3D SourceRight { get; }
}
