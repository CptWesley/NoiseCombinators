using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoiseCombinators.NoiseGenerators3D.Modifiers.Unary;

/// <summary>
/// Provides a noise generator which has shifted inputs.
/// </summary>
public sealed class ShiftedNoise3D : UnaryNoiseBase3D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShiftedNoise3D"/> class.
    /// </summary>
    /// <param name="source">The noise generator to invert.</param>
    /// <param name="shiftX">The amount of shift on the x-axis.</param>
    /// <param name="shiftY">The amount of shift on the y-axis.</param>
    /// <param name="shiftZ">The amount of shift on the z-axis.</param>
    public ShiftedNoise3D(INoise3D source, double shiftX, double shiftY, double shiftZ)
        : base(source)
    {
        ShiftX = shiftX;
        ShiftY = shiftY;
        ShiftZ = shiftZ;
    }

    /// <summary>
    /// Gets the amount of shift on the x-axis.
    /// </summary>
    public double ShiftX { get; }

    /// <summary>
    /// Gets the amount of shift on the y-axis.
    /// </summary>
    public double ShiftY { get; }

    /// <summary>
    /// Gets the amount of shift on the y-axis.
    /// </summary>
    public double ShiftZ { get; }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double[][][] GetChunkWithSeed(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeed(seed, x + ShiftX, y + ShiftY, z + ShiftZ, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double[][][] GetChunk(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunk(x + ShiftX, y + ShiftY, z + ShiftZ, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Task<double[][][]> GetChunkWithSeedAsync(int seed, double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkWithSeedAsync(seed, x + ShiftX, y + ShiftY, z + ShiftZ, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Task<double[][][]> GetChunkAsync(double x, double y, double z, int stepsX, int stepsY, int stepsZ, double stepSizeX, double stepSizeY, double stepSizeZ)
        => Source.GetChunkAsync(x + ShiftX, y + ShiftY, z + ShiftZ, stepsX, stepsY, stepsZ, stepSizeX, stepSizeY, stepSizeZ);
}
