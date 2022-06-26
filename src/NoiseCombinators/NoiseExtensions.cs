using System;
using System.Runtime.CompilerServices;
using NoiseCombinators.NoiseGenerators.Modifiers;

namespace NoiseCombinators;

/// <summary>
/// Provides extension methods for building noise generators.
/// </summary>
public static class NoiseExtensions
{
    /// <summary>
    /// Changes the range of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetRange(this INoise noise, double min, double max)
        => noise.SetRange(n => min, n => max);

    /// <summary>
    /// Changes the range of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetRange(this INoise noise, Func<INoise, double> min, Func<INoise, double> max)
        => new RangedNoise(noise, min(noise), max(noise));

    /// <summary>
    /// Changes the minimum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetMin(this INoise noise, Func<INoise, double> min)
        => noise.SetRange(n => min(n), n => n.Max);

    /// <summary>
    /// Changes the minimum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetMin(this INoise noise, double min)
        => noise.SetMin(n => min);

    /// <summary>
    /// Changes the maximum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetMax(this INoise noise, Func<INoise, double> max)
        => noise.SetRange(n => n.Min, n => max(n));

    /// <summary>
    /// Changes the maximum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise SetMax(this INoise noise, double max)
        => noise.SetMax(n => max);

    /// <summary>
    /// Normalizes the range of the noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Normalize(this INoise noise)
        => noise.SetRange(0, 1);

    /// <summary>
    /// Applies a kernel filter to the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="kernel">The kernel to apply.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise ApplyKernelFilter(this INoise noise, double[][] kernel)
        => new KernelFilterNoise(noise, kernel);

    /// <summary>
    /// Modifies the scale of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scale">The scale modifier to apply.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Scale(this INoise noise, double scale)
        => new ScaledNoise(noise, scale);

    /// <summary>
    /// Changes the range of the given noise generator by clamping.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Clamp(this INoise noise, double min, double max)
        => noise.Clamp(n => min, n => max);

    /// <summary>
    /// Changes the range of the given noise generator by clamping.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Clamp(this INoise noise, Func<INoise, double> min, Func<INoise, double> max)
        => new ClampedNoise(noise, min(noise), max(noise));

    /// <summary>
    /// Inverts the values produced by the noise generator within its original range.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Invert(this INoise noise)
        => new InvertedNoise(noise);

    /// <summary>
    /// Shifts the noise generator with the given amount on the axes.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="x">The amount of shift on the x-axis.</param>
    /// <param name="y">The amount of shift on the y-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Shift(this INoise noise, double x, double y)
        => new ShiftedNoise(noise, x, y);
}
