namespace NoiseCombinators;

/// <summary>
/// Provides an interface for 2 dimensional noise functions.
/// </summary>
public interface INoise
{
    /// <summary>
    /// Gets the lower bound value of the noise produced by this noise generator.
    /// </summary>
    public double Min { get; }

    /// <summary>
    /// Gets the upper bound value of the noise produced by this noise generator.
    /// </summary>
    public double Max { get; }

    /// <summary>
    /// Gets the value at the given coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The value at the given coordinate.</returns>
    public double Get(double x, double y);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSizeX">The size of each step in the x-axis.</param>
    /// <param name="stepSizeY">The size of each step in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSize">The size of each step in each axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunk(double x, double y, int stepsX, int stepsY, double stepSize);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunk(double x, double y, int stepsX, int stepsY);
}
