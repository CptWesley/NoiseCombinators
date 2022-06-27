namespace NoiseCombinators.Hashing;

/// <summary>
/// Provides an interface for hashing functions that produce 64 bit hashes.
/// </summary>
public interface IHashing32
{
    /// <summary>
    /// Hashes an unsigned int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public int Hash32(uint value);

    /// <summary>
    /// Hashes a signed int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public int Hash32(int value);

    /// <summary>
    /// Hashes an unsigned int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public uint HashU32(uint value);

    /// <summary>
    /// Hashes a signed int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public uint HashU32(int value);

    /// <summary>
    /// Hashes an unsigned int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public int Hash32WithSeed(int seed, uint value);

    /// <summary>
    /// Hashes a signed int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public int Hash32WithSeed(int seed, int value);

    /// <summary>
    /// Hashes an unsigned int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public uint HashU32WithSeed(int seed, uint value);

    /// <summary>
    /// Hashes a signed int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public uint HashU32WithSeed(int seed, int value);
}
