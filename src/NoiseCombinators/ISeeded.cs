namespace NoiseCombinators;

/// <summary>
/// Interface for everything with a seed.
/// </summary>
public interface ISeeded
{
    /// <summary>
    /// Gets the seed of the hashing function.
    /// </summary>
    public int Seed { get; }
}
