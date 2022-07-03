using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NoiseCombinators.NoiseGenerators2D.Modifiers.Binary;
using NoiseCombinators.NoiseGenerators2D.Modifiers.Unary;

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
    public static INoise2D SetRange(this INoise2D noise, double min, double max)
        => noise.SetRange(n => min, n => max);

    /// <summary>
    /// Changes the range of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D SetRange(this INoise2D noise, Func<INoise2D, double> min, Func<INoise2D, double> max)
        => new RangedNoise2D(noise, min(noise), max(noise));

    /// <summary>
    /// Changes the minimum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D SetMin(this INoise2D noise, Func<INoise2D, double> min)
        => noise.SetRange(n => min(n), n => n.Max);

    /// <summary>
    /// Changes the minimum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D SetMin(this INoise2D noise, double min)
        => noise.SetMin(n => min);

    /// <summary>
    /// Changes the maximum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D SetMax(this INoise2D noise, Func<INoise2D, double> max)
        => noise.SetRange(n => n.Min, n => max(n));

    /// <summary>
    /// Changes the maximum of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D SetMax(this INoise2D noise, double max)
        => noise.SetMax(n => max);

    /// <summary>
    /// Normalizes the range of the noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Normalize(this INoise2D noise)
        => noise.SetRange(0, 1);

    /// <summary>
    /// Applies a kernel filter to the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="kernel">The kernel to apply.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D ApplyKernelFilter(this INoise2D noise, Kernel2D kernel)
        => new KernelFilterNoise2D(noise, kernel);

    /// <summary>
    /// Modifies the scale of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scaleX">The scale modifier to apply on the x-axis.</param>
    /// <param name="scaleY">The scale modifier to apply on the y-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Scale(this INoise2D noise, double scaleX, double scaleY)
        => new ScaledNoise2D(noise, scaleX, scaleY);

    /// <summary>
    /// Modifies the scale of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scale">The scale modifier to apply on the x-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Scale(this INoise2D noise, double scale)
        => noise.Scale(scale, scale);

    /// <summary>
    /// Changes the range of the given noise generator by clamping.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Clamp(this INoise2D noise, double min, double max)
        => noise.Clamp(n => min, n => max);

    /// <summary>
    /// Changes the range of the given noise generator by clamping.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="min">A function to compute the new minimum value.</param>
    /// <param name="max">A function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Clamp(this INoise2D noise, Func<INoise2D, double> min, Func<INoise2D, double> max)
        => new ClampedNoise2D(noise, min(noise), max(noise));

    /// <summary>
    /// Inverts the values produced by the noise generator within its original range.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Invert(this INoise2D noise)
        => new InvertedNoise2D(noise);

    /// <summary>
    /// Shifts the noise generator with the given amount on the axes.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="x">The amount of shift on the x-axis.</param>
    /// <param name="y">The amount of shift on the y-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Shift(this INoise2D noise, double x, double y)
        => new ShiftedNoise2D(noise, x, y);

    /// <summary>
    /// Adds the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Add(this INoise2D noise, INoise2D other)
        => new AddedNoise2D(noise, other);

    /// <summary>
    /// Adds the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Add(this INoise2D noise, IEnumerable<INoise2D> others)
    {
        INoise2D result = noise;

        foreach (INoise2D other in others)
        {
            result = result.Add(other);
        }

        return result;
    }

    /// <summary>
    /// Adds the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Add(this INoise2D noise, params INoise2D[] others)
        => noise.Add((IEnumerable<INoise2D>)others);

    /// <summary>
    /// Adds a scalar to the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The added scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Add(this INoise2D noise, double scalar)
        => new AddedScalarNoise2D(noise, scalar);

    /// <summary>
    /// Subtracts the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Subtract(this INoise2D noise, INoise2D other)
        => new SubtractedNoise2D(noise, other);

    /// <summary>
    /// Subtracts the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Subtract(this INoise2D noise, IEnumerable<INoise2D> others)
    {
        INoise2D result = noise;

        foreach (INoise2D other in others)
        {
            result = result.Subtract(other);
        }

        return result;
    }

    /// <summary>
    /// Subtracts the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Subtract(this INoise2D noise, params INoise2D[] others)
        => noise.Subtract((IEnumerable<INoise2D>)others);

    /// <summary>
    /// Subtracts a scalar from the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The subtracted scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Subtract(this INoise2D noise, double scalar)
        => new SubtractedScalarNoise2D(noise, scalar);

    /// <summary>
    /// Multiplies the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Multiply(this INoise2D noise, INoise2D other)
        => new MultipliedNoise2D(noise, other);

    /// <summary>
    /// Multiplies the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Multiply(this INoise2D noise, IEnumerable<INoise2D> others)
    {
        INoise2D result = noise;

        foreach (INoise2D other in others)
        {
            result = result.Multiply(other);
        }

        return result;
    }

    /// <summary>
    /// Multiplies the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Multiply(this INoise2D noise, params INoise2D[] others)
        => noise.Multiply((IEnumerable<INoise2D>)others);

    /// <summary>
    /// Multiplies a scalar with the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The multiplication scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Multiply(this INoise2D noise, double scalar)
        => new MultipliedScalarNoise2D(noise, scalar);

    /// <summary>
    /// Applies a function to every value obtained from the given noise generator.
    /// Note that this is a very powerful combinator which allows you to violate the
    /// upper and lower bounds if not configured properly.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="func">The function to apply.</param>
    /// <param name="min">The function to compute the new minimum value.</param>
    /// <param name="max">The function to compute the new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Apply(this INoise2D noise, Func<double, double> func, Func<INoise2D, double> min, Func<INoise2D, double> max)
        => new AppliedLambdaNoise2D(noise, func, min, max);

    /// <summary>
    /// Applies a function to every value obtained from the given noise generator.
    /// Note that this is a very powerful combinator which allows you to violate the
    /// upper and lower bounds if not configured properly.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="func">The function to apply.</param>
    /// <param name="min">The new minimum value.</param>
    /// <param name="max">The new maximum value.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D Apply(this INoise2D noise, Func<double, double> func, double min, double max)
        => noise.Apply(func, n => min, n => max);

    /// <summary>
    /// Applies a sigmoid function to the output of the given noise generator.
    /// This makes the extreme values more extreme, while staying within the given boundaries.
    /// This is useful to counteract the effects of normalization.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="gradient">Controls the slope at the center of the sigmoid function.</param>
    /// <param name="power">Controls the shape of the curve. Higher power causes a more rectangular shape.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D ApplySigmoid(this INoise2D noise, double gradient, double power)
    {
        if (Math.Abs(power) < double.Epsilon)
        {
            throw new ArgumentException($"Power may not be zero.", nameof(power));
        }

        double powerInv = 1d / power;

        double min = noise.Min;
        double max = noise.Max;
        double range = max - min;
        double r = range / 2d;
        double s = Math.Abs(gradient);
        double sgn = Math.Sign(gradient);
        if (sgn == 0)
        {
            sgn = 1;
        }

        double rst = r * s;
        double rstPow = Math.Pow(rst, power);
        double rd1 = sgn * Math.Pow(1 + rstPow, powerInv);
        double rs = r + min;

        return noise.Apply(
            v =>
            {
                double vrs = v - rs;
                double z = s * vrs;
                double zPow = Math.Pow(Math.Abs(z), power);
                double result = (rd1 * vrs / Math.Pow(1 + zPow, powerInv)) + rs;
                return result;
            },
            min,
            max);
    }

    /// <summary>
    /// Sets the seed of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="seed">The new seed.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D WithSeed(this INoise2D noise, int seed)
        => new SeededNoise2D(noise, seed);

    /// <summary>
    /// Modifies a seed supplied to the noise generator containing this noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="seed">The seed modification.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise2D WithSeed(this INoise2D noise, Func<int, int> seed)
        => new SeededLambdaNoise2D(noise, seed);
}
