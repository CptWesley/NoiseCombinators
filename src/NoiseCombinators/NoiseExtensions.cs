using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NoiseCombinators.NoiseGenerators.Modifiers.Binary;
using NoiseCombinators.NoiseGenerators.Modifiers.Unary;

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
    public static INoise ApplyKernelFilter(this INoise noise, Kernel kernel)
        => new KernelFilterNoise(noise, kernel);

    /// <summary>
    /// Modifies the scale of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scaleX">The scale modifier to apply on the x-axis.</param>
    /// <param name="scaleY">The scale modifier to apply on the y-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Scale(this INoise noise, double scaleX, double scaleY)
        => new ScaledNoise(noise, scaleX, scaleY);

    /// <summary>
    /// Modifies the scale of the given noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scale">The scale modifier to apply on the x-axis.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Scale(this INoise noise, double scale)
        => noise.Scale(scale, scale);

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

    /// <summary>
    /// Adds the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Add(this INoise noise, INoise other)
        => new AddedNoise(noise, other);

    /// <summary>
    /// Adds the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Add(this INoise noise, IEnumerable<INoise> others)
    {
        INoise result = noise;

        foreach (INoise other in others)
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
    public static INoise Add(this INoise noise, params INoise[] others)
        => noise.Add((IEnumerable<INoise>)others);

    /// <summary>
    /// Adds a scalar to the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The added scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Add(this INoise noise, double scalar)
        => new AddedScalarNoise(noise, scalar);

    /// <summary>
    /// Subtracts the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Subtract(this INoise noise, INoise other)
        => new SubtractedNoise(noise, other);

    /// <summary>
    /// Subtracts the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Subtract(this INoise noise, IEnumerable<INoise> others)
    {
        INoise result = noise;

        foreach (INoise other in others)
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
    public static INoise Subtract(this INoise noise, params INoise[] others)
        => noise.Subtract((IEnumerable<INoise>)others);

    /// <summary>
    /// Subtracts a scalar from the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The subtracted scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Subtract(this INoise noise, double scalar)
        => new SubtractedScalarNoise(noise, scalar);

    /// <summary>
    /// Multiplies the results of two noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="other">The other noise generator.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Multiply(this INoise noise, INoise other)
        => new MultipliedNoise(noise, other);

    /// <summary>
    /// Multiplies the results of multiple noise generators.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="others">The other noise generators.</param>
    /// <returns>The combined noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Multiply(this INoise noise, IEnumerable<INoise> others)
    {
        INoise result = noise;

        foreach (INoise other in others)
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
    public static INoise Multiply(this INoise noise, params INoise[] others)
        => noise.Multiply((IEnumerable<INoise>)others);

    /// <summary>
    /// Multiplies a scalar with the result of a noise generator.
    /// </summary>
    /// <param name="noise">The noise generator.</param>
    /// <param name="scalar">The multiplication scalar.</param>
    /// <returns>The modified noise generator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static INoise Multiply(this INoise noise, double scalar)
        => new MultipliedScalarNoise(noise, scalar);

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
    public static INoise Apply(this INoise noise, Func<double, double> func, Func<INoise, double> min, Func<INoise, double> max)
        => new AppliedLambdaNoise(noise, func, min, max);

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
    public static INoise Apply(this INoise noise, Func<double, double> func, double min, double max)
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
    public static INoise ApplySigmoid(this INoise noise, double gradient, double power)
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
}
