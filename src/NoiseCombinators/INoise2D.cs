﻿using System.Threading.Tasks;

namespace NoiseCombinators;

/// <summary>
/// Provides an interface for 2 dimensional noise functions.
/// </summary>
public interface INoise2D
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
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSizeX">The size of each step in the x-axis.</param>
    /// <param name="stepSizeY">The size of each step in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSize">The size of each step in each axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY, double stepSize);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public double[][] GetChunkWithSeed(int seed, double x, double y, int stepsX, int stepsY);

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

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSizeX">The size of each step in the x-axis.</param>
    /// <param name="stepSizeY">The size of each step in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSize">The size of each step in each axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY, double stepSize);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="seed">The seed to use for generating the noise.</param>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public Task<double[][]> GetChunkWithSeedAsync(int seed, double x, double y, int stepsX, int stepsY);

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
    public Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSizeX, double stepSizeY);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <param name="stepSize">The size of each step in each axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY, double stepSize);

    /// <summary>
    /// Gets a chunk of values at the given coordinates.
    /// </summary>
    /// <param name="x">The starting x-coordinate.</param>
    /// <param name="y">The starting y-coordinate.</param>
    /// <param name="stepsX">The number of steps to take in the x-axis.</param>
    /// <param name="stepsY">The number of steps to take in the y-axis.</param>
    /// <returns>The generated noise chunk.</returns>
    public Task<double[][]> GetChunkAsync(double x, double y, int stepsX, int stepsY);
}
