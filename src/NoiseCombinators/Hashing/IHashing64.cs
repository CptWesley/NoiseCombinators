namespace NoiseCombinators.Hashing;

/// <summary>
/// Provides an interface for hashing functions that produce 64 bit hashes.
/// </summary>
public interface IHashing64 : ISeeded
{
    /// <summary>
    /// Hashes an unsigned long value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64(ulong value);

    /// <summary>
    /// Hashes a signed long value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64(long value);

    /// <summary>
    /// Hashes an unsigned int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64(uint value);

    /// <summary>
    /// Hashes a signed int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64(int value);

    /// <summary>
    /// Hashes an unsigned long value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64(ulong value);

    /// <summary>
    /// Hashes a signed long value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64(long value);

    /// <summary>
    /// Hashes an unsigned int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64(uint value);

    /// <summary>
    /// Hashes a signed int value.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64(int value);

    /// <summary>
    /// Hashes an unsigned long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, ulong value);

    /// <summary>
    /// Hashes a signed long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, long value);

    /// <summary>
    /// Hashes an unsigned int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, uint value);

    /// <summary>
    /// Hashes a signed int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, int value);

    /// <summary>
    /// Hashes an unsigned long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64WithSeed(int seed, ulong value);

    /// <summary>
    /// Hashes a signed long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64WithSeed(int seed, long value);

    /// <summary>
    /// Hashes an unsigned int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64WithSeed(int seed, uint value);

    /// <summary>
    /// Hashes a signed int value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public long Hash64WithSeed(int seed, int value);
}
