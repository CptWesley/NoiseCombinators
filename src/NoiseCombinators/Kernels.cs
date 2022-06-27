namespace NoiseCombinators;

/// <summary>
/// Provides some common kernels.
/// </summary>
public static class Kernels
{
    /// <summary>
    /// Gets a discrete 3x3 Gaussian kernel.
    /// </summary>
    public static readonly Kernel Gaussian3 = new Kernel(
        new double[][]
        {
            new double[] { 1, 2, 1 },
            new double[] { 2, 4, 2 },
            new double[] { 1, 2, 1 },
        },
        16);

    /// <summary>
    /// Gets a discrete 5x5 Gaussian kernel.
    /// </summary>
    public static readonly Kernel Gaussian5 = new Kernel(
        new double[][]
        {
            new double[] { 1,  4,  7,  4, 1 },
            new double[] { 4, 16, 26, 16, 4 },
            new double[] { 7, 26, 41, 26, 7 },
            new double[] { 4, 16, 26, 16, 4 },
            new double[] { 1,  4,  7,  4, 1 },
        },
        273);

    /// <summary>
    /// Gets a discrete 7x7 Gaussian kernel.
    /// </summary>
    public static readonly Kernel Gaussian7 = new Kernel(
        new double[][]
        {
            new double[] { 0,  0,  1,   2,  1,  0, 0 },
            new double[] { 0,  3, 13,  22, 13,  3, 0 },
            new double[] { 1, 13, 59,  97, 59, 13, 1 },
            new double[] { 2, 22, 97, 159, 97, 22, 2 },
            new double[] { 1, 13, 59,  97, 59, 13, 1 },
            new double[] { 0,  3, 13,  22, 13,  3, 0 },
            new double[] { 0,  0,  1,   2,  1,  0, 0 },
        },
        1003);
}
