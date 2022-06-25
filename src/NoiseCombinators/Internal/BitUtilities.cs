using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators.Internal;

/// <summary>
/// Provides helper functions related to bits.
/// </summary>
internal static class BitUtilities
{
    /// <summary>
    /// Rotates the value a number of bits.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate.</param>
    /// <returns>The result of the bit rotation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong RotateLeft(ulong value, int offset)
        => (value << offset) | (value >> (64 - offset));

    /// <summary>
    /// Rounds a floating point value to its floor.
    /// </summary>
    /// <param name="x">The value to find the floor of.</param>
    /// <returns>The floor of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FastFloor(double x)
    {
        int xi = (int)x;
        return x < xi ? xi - 1 : xi;
    }

    /// <summary>
    /// Rounds a floating point value to its ceiling.
    /// </summary>
    /// <param name="x">The value to find the ceiling of.</param>
    /// <returns>The ceiling of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FastCeiling(double x)
    {
        int xi = (int)x;
        return x < xi ? xi : xi + 1;
    }

    /// <summary>
    /// Checks that two floating point numbers are almost equal.
    /// </summary>
    /// <param name="a">The left double.</param>
    /// <param name="b">The right double.</param>
    /// <param name="epsilon">The allowed error margin.</param>
    /// <returns>A value indicating whether or not the two floating point numbers are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEqual(double a, double b, double epsilon = double.Epsilon * 10000)
        => Math.Abs(a - b) < epsilon;
}
