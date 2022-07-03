﻿namespace NoiseCombinators.Hashing;

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
    /// Hashes an unsigned long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value">The value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, ulong value);

    /// <summary>
    /// Hashes an unsigned long value.
    /// </summary>
    /// <param name="value1">The first 32 bits of the value to hash.</param>
    /// <param name="value2">The last 32 bits of the value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64(ulong value1, ulong value2);

    /// <summary>
    /// Hashes an unsigned long value using the given seed
    /// rather than the default hash set in the class.
    /// </summary>
    /// <param name="seed">The seed to use.</param>
    /// <param name="value1">The first 32 bits of the value to hash.</param>
    /// <param name="value2">The last 32 bits of the value to hash.</param>
    /// <returns>The hash.</returns>
    public ulong HashU64WithSeed(int seed, ulong value1, ulong value2);
}
