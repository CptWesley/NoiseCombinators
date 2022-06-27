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
    public long Hash64(ulong value)
        => unchecked((long)HashU64(value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64(long value)
        => unchecked((long)HashU64(unchecked((ulong)value)));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64(uint value)
        => unchecked((long)HashU64(unchecked((ulong)value)));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64(int value)
        => unchecked((long)HashU64(unchecked((ulong)value)));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual ulong HashU64(ulong value)
        => HashU64WithSeed(Seed, value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64(long value)
        => HashU64(unchecked((ulong)value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64(uint value)
        => HashU64(unchecked((ulong)value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64(int value)
        => HashU64(unchecked((ulong)value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64WithSeed(int seed, ulong value)
        => unchecked((long)HashU64WithSeed(seed, value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64WithSeed(int seed, long value)
        => unchecked((long)HashU64WithSeed(seed, unchecked((ulong)value)));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64WithSeed(int seed, uint value)
        => unchecked((long)HashU64WithSeed(seed, unchecked((ulong)value)));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Hash64WithSeed(int seed, int value)
        => unchecked((long)HashU64WithSeed(seed, unchecked((ulong)value)));

    /// <inheritdoc/>
    public abstract ulong HashU64WithSeed(int seed, ulong value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64WithSeed(int seed, long value)
        => HashU64WithSeed(seed, unchecked((ulong)value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64WithSeed(int seed, uint value)
        => HashU64WithSeed(seed, unchecked((ulong)value));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong HashU64WithSeed(int seed, int value)
        => HashU64WithSeed(seed, unchecked((ulong)value));
}
