using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.Hashing;

/// <summary>
/// Provides methods to create hashing functions.
/// </summary>
public static class HashingFactory
{
    /// <summary>
    /// Creates a hashing function.
    /// </summary>
    /// <param name="algorithm">The algorithm to use.</param>
    /// <param name="seed">The seed to use.</param>
    /// <returns>The created hashing function.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IHashing64 Create(Hashing64Algorithm algorithm, int seed)
        => algorithm switch
        {
            Hashing64Algorithm.XXHash => new XXHash(seed),
            _ => throw new ArgumentException($"Unknown 64 bit hashing algorithm: '{algorithm}'."),
        };

    /// <summary>
    /// Creates a hashing function.
    /// </summary>
    /// <param name="algorithm">The algorithm to use.</param>
    /// <returns>The created hashing function.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IHashing64 Create(Hashing64Algorithm algorithm)
        => Create(algorithm, Environment.TickCount);
}
