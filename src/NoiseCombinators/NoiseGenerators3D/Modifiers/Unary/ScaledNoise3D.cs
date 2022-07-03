using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a scaled view of a given source noise generator.
/// </summary>
public sealed class ScaledNoise3D : UnaryNoiseBase3D
{
    private readonly double scaleXInv;
    private readonly double scaleYInv;
    private readonly double scaleZInv;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScaledNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to scale.</param>
    /// <param name="scaleX">The scale to scale with on the x-axis.</param>
    /// <param name="scaleY">The scale to scale with on the y-axis.</param>
    /// <param name="scaleZ">The scale to scale with on the z-axis.</param>
    public ScaledNoise3D(INoise3D source, double scaleX, double scaleY, double scaleZ)
        : base(source)
    {
        ScaleX = scaleX;
        ScaleY = scaleY;
        ScaleZ = scaleZ;
        scaleXInv = 1d / scaleX;
        scaleYInv = 1d / scaleY;
        scaleZInv = 1d / scaleZ;
    }

    /// <summary>
    /// Gets the scale used when sampling the source generator on the x-axis.
    /// </summary>
    public double ScaleX { get; }

    /// <summary>
    /// Gets the scale used when sampling the source generator on the y-axis.
    /// </summary>
    public double ScaleY { get; }

    /// <summary>
    /// Gets the scale used when sampling the source generator on the z-axis.
    /// </summary>
    public double ScaleZ { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeed(seed, x * scaleXInv, y * scaleYInv, z * scaleZInv, stepsX, stepsY, stepsZ, stepSizeX * scaleXInv, stepSizeY * scaleYInv, stepSizeZ * scaleZInv);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunk(x * scaleXInv, y * scaleYInv, z * scaleZInv, stepsX, stepsY, stepsZ, stepSizeX * scaleXInv, stepSizeY * scaleYInv, stepSizeZ * scaleZInv);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeedAsync(seed, x * scaleXInv, y * scaleYInv, z * scaleZInv, stepsX, stepsY, stepsZ, stepSizeX * scaleXInv, stepSizeY * scaleYInv, stepSizeZ * scaleZInv);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sealed Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkAsync(x * scaleXInv, y * scaleYInv, z * scaleZInv, stepsX, stepsY, stepsZ, stepSizeX * scaleXInv, stepSizeY * scaleYInv, stepSizeZ * scaleZInv);
}
