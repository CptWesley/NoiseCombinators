using System;
using System.Runtime.CompilerServices;

namespace NoiseCombinators;

/// <summary>
/// Provides logic for kernels.
/// </summary>
public sealed class Kernel
{
    private readonly double[] raw;

    /// <summary>
    /// Initializes a new instance of the <see cref="Kernel"/> class.
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    /// <param name="divisor">The divisor.</param>
    public Kernel(double[][] kernel, double divisor)
    {
        raw = CopyKernel(kernel);
        Divisor = divisor;

        double sum = 0;
        foreach (double value in raw)
        {
            sum += value;
        }

        Sum = sum;
        Max = sum / divisor;
        Width = kernel.Length;

        if (Width <= 0)
        {
            throw new ArgumentException("Kernel dimensions need to be at least 1x1.", nameof(kernel));
        }

        Height = kernel[0].Length;

        if (Height <= 0)
        {
            throw new ArgumentException("Kernel dimensions need to be at least 1x1.", nameof(kernel));
        }
    }

    /// <summary>
    /// Gets the width of the kernel.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the kernel.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the divisor of the kernel.
    /// </summary>
    public double Divisor { get; }

    /// <summary>
    /// Gets the sum of the kernel.
    /// </summary>
    public double Sum { get; }

    /// <summary>
    /// Gets the maximum value.
    /// </summary>
    public double Max { get; }

    /// <summary>
    /// Retrieves a value from the kernel.
    /// </summary>
    /// <param name="x">The x-coordinate in the kernel.</param>
    /// <param name="y">The y-coordinate in the kernel.</param>
    /// <returns>The value in the kernel.</returns>
    public double this[int x, int y]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => raw[(x * Height) + y];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double[] CopyKernel(double[][] kernel)
    {
        int width = kernel.Length;
        int height = kernel[0].Length;

        double[] result = new double[width * height];

        for (int x = 0; x < kernel.Length; x++)
        {
            double[] col = kernel[x];

            for (int y = 0; y < col.Length; y++)
            {
                result[(x * height) + y] = col[y];
            }
        }

        return result;
    }
}
