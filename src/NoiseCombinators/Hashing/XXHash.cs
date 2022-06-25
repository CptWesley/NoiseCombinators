using System.Runtime.CompilerServices;
using NoiseCombinators.Internal;

namespace NoiseCombinators.Hashing;

/// <summary>
/// Implements <see href="https://cyan4973.github.io/xxHash/">xxHash</see>.
/// Implementation is based on the implementation provided in <see href="https://github.com/ssg/HashDepot">HashDepot</see>.
/// </summary>
public sealed class XXHash : Hashing64
{
    private const ulong PrimeU64v1 = 11400714785074694791ul;
    private const ulong PrimeU64v2 = 14029467366897019727ul;
    private const ulong PrimeU64v3 = 1609587929392839161ul;
    private const ulong PrimeU64v4 = 9650029242287828579ul;
    private const ulong PrimeU64v5 = 2870177450012600261ul;

    /// <summary>
    /// Initializes a new instance of the <see cref="XXHash"/> class.
    /// </summary>
    /// <param name="seed">The seed of this hashing function.</param>
    public XXHash(int seed)
        : base(seed)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XXHash"/> class.
    /// </summary>
    public XXHash()
        : base()
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ulong HashU64(ulong value)
    {
        ulong acc = PrimeU64v5 + unchecked((ulong)Seed);

        ulong accn = acc + (value * PrimeU64v2);
        acc ^= BitUtilities.RotateLeft(accn, 31) * PrimeU64v1;
        acc = BitUtilities.RotateLeft(acc, 27) * PrimeU64v1;
        acc += PrimeU64v4;

        acc ^= acc >> 33;
        acc *= PrimeU64v2;
        acc ^= acc >> 29;
        acc *= PrimeU64v3;
        acc ^= acc >> 32;

        return acc;
    }
}
