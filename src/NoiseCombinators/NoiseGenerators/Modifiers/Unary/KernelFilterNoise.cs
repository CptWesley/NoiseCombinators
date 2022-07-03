using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators.Modifiers.Unary;

/// <summary>
/// Provides a noise generator that applies a kernel filter on an existing noise generator.
/// </summary>
public sealed class KernelFilterNoise : UnaryNoiseBase
{
    private readonly int extraWidth;
    private readonly int extraHeight;
    private readonly int extraWidth2;
    private readonly int extraHeight2;

    /// <summary>
    /// Initializes a new instance of the <see cref="KernelFilterNoise"/> class.
    /// </summary>
    /// <param name="source">The base noise generator.</param>
    /// <param name="kernel">The kernel to apply.</param>
    public KernelFilterNoise(INoise source, Kernel kernel)
        : base(source)
    {
        Kernel = kernel;
        extraWidth = Kernel.Width / 2;
        extraHeight = Kernel.Height / 2;
        extraWidth2 = extraWidth * 2;
        extraHeight2 = extraHeight * 2;

        double mi = source.Min * Kernel.Max;
        double ma = source.Max * Kernel.Max;

        if (mi > ma)
        {
            Max = mi;
            Min = ma;
        }
        else
        {
            Max = ma;
            Min = mi;
        }
    }

    /// <summary>
    /// Gets the kernel.
    /// </summary>
    public Kernel Kernel { get; }

    /// <inheritdoc/>
    public override sealed double Min { get; }

    /// <inheritdoc/>
    public override sealed double Max { get; }

    /// <inheritdoc/>
    public override sealed double[][] GetChunkWithSeed2D(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double newStartX = x - (extraWidth * stepSizeX);
        double newStartY = y - (extraHeight * stepSizeY);
        int newStepsX = stepsX + extraWidth2;
        int newStepsY = stepsY + extraHeight2;

        double[][] values = Source.GetChunkWithSeed2D(seed, newStartX, newStartY, newStepsX, newStepsY, stepSizeX, stepSizeY);
        return Apply(values, stepsX, stepsY);
    }

    /// <inheritdoc/>
    public override sealed double[][] GetChunk2D(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double newStartX = x - (extraWidth * stepSizeX);
        double newStartY = y - (extraHeight * stepSizeY);
        int newStepsX = stepsX + extraWidth2;
        int newStepsY = stepsY + extraHeight2;

        double[][] values = Source.GetChunk2D(newStartX, newStartY, newStepsX, newStepsY, stepSizeX, stepSizeY);
        return Apply(values, stepsX, stepsY);
    }

    /// <inheritdoc/>
    public override sealed async Task<double[][]> GetChunkWithSeed2DAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double newStartX = x - (extraWidth * stepSizeX);
        double newStartY = y - (extraHeight * stepSizeY);
        int newStepsX = stepsX + extraWidth2;
        int newStepsY = stepsY + extraHeight2;

        double[][] values = await Source.GetChunkWithSeed2DAsync(seed, newStartX, newStartY, newStepsX, newStepsY, stepSizeX, stepSizeY).ConfigureAwait(false);
        return Apply(values, stepsX, stepsY);
    }

    /// <inheritdoc/>
    public override sealed async Task<double[][]> GetChunk2DAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY)
    {
        double newStartX = x - (extraWidth * stepSizeX);
        double newStartY = y - (extraHeight * stepSizeY);
        int newStepsX = stepsX + extraWidth2;
        int newStepsY = stepsY + extraHeight2;

        double[][] values = await Source.GetChunk2DAsync(newStartX, newStartY, newStepsX, newStepsY, stepSizeX, stepSizeY).ConfigureAwait(false);
        return Apply(values, stepsX, stepsY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private double[][] Apply(double[][] values, int width, int height)
    {
        double[][] result = new double[width][];

        for (int ix = 0; ix < width; ix++)
        {
            double[] col = new double[height];
            result[ix] = col;

            for (int iy = 0; iy < height; iy++)
            {
                double value = 0;

                for (int kx = 0; kx < Kernel.Width; kx++)
                {
                    double[] valuesCol = values[ix + kx];
                    for (int ky = 0; ky < Kernel.Height; ky++)
                    {
                        value += Kernel[kx, ky] * valuesCol[iy + ky];
                    }
                }

                col[iy] = value / Kernel.Divisor;
            }
        }

        return result;
    }
}
