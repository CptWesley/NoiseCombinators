using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.Hashing;

/// <summary>
/// Provides implementations for most interface methods.
/// </summary>
public abstract class Hashing64 : IHashing64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Hashing64"/> class.
    /// </summary>
    /// <param name="seed">The seed of this hashing function.</param>
    public Hashing64(int seed)
    {
        Seed = seed;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Hashing64"/> class.
    /// </summary>
    public Hashing64()
        : this(Environment.TickCount)
    {
    }

    /// <inheritdoc/>
    public int Seed { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual ulong HashU64(ulong value)
        => HashU64WithSeed(Seed, value);

    /// <inheritdoc/>
    public abstract ulong HashU64WithSeed(int seed, ulong value);
}
