namespace NoiseCombinators;

/// <summary>
/// Provides some common kernels.
/// </summary>
public static class Kernels
{
    /// <summary>
    /// Creates a discrete Gaussian kernel of size 3.
    /// </summary>
    /// <returns>The discrete Gaussian kernel of size 3.</returns>
    public static double[][] Gaussian3()
    {
        return new double[][]
        {
            new double[] { 1d / 16, 1d / 8, 1d / 16 },
            new double[] { 1d / 8, 1d / 4, 1d / 8 },
            new double[] { 1d / 16, 1d / 8, 1d / 16 },
        };
    }

    /// <summary>
    /// Creates a discrete Gaussian kernel of size 5.
    /// </summary>
    /// <returns>The discrete Gaussian kernel of size 5.</returns>
    public static double[][] Gaussian5()
    {
        return new double[][]
        {
            new double[] { 1 / 273d,  4 / 273d,  7 / 273d,  4 / 273d, 1 / 273d },
            new double[] { 4 / 273d, 16 / 273d, 26 / 273d, 16 / 273d, 1 / 273d },
            new double[] { 7 / 273d, 26 / 273d, 41 / 273d, 26 / 273d, 1 / 273d },
            new double[] { 4 / 273d, 14 / 273d, 26 / 273d, 16 / 273d, 1 / 273d },
            new double[] { 1 / 273d,  4 / 273d,  7 / 273d,  4 / 273d, 1 / 273d },
        };
    }

    /// <summary>
    /// Creates a discrete box kernel of the given size.
    /// </summary>
    /// <param name="size">The size of the box kernel.</param>
    /// <returns>The discrete Gaussian kernel of the given size.</returns>
    public static double[][] Box(int size)
        => Box(size, size);

    /// <summary>
    /// Creates a discrete box kernel of the given size.
    /// </summary>
    /// <param name="width">The width of the box kernel.</param>
    /// <param name="height">The height of the box kernel.</param>
    /// <returns>The discrete Gaussian kernel of the given size.</returns>
    public static double[][] Box(int width, int height)
    {
        int size = width * height;
        double value = 1d / size;
        double[][] result = new double[width][];

        for (int x = 0; x < width; x++)
        {
            double[] col = new double[height];
            result[x] = col;

            for (int y = 0; y < height; y++)
            {
                col[y] = value;
            }
        }

        return result;
    }
}
