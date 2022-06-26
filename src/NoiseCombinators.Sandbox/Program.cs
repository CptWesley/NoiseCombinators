using NoiseCombinators.Hashing;
using NoiseCombinators.NoiseGenerators;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NoiseCombinators.Sandbox;

public static class Program
{
    public static void Main()
    {
        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 0.02;
        INoise noise = new KernelFilterNoise(new RangedNoise(new BicubicNoise(42), 0, 1), Kernels.Gaussian5());
        double[][] data = noise.GetChunk(-sizeHalf * scale, -sizeHalf * scale, size, size, scale);
        SaveAsImage(data, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png");
    }

    private static void SaveAsImage(double[][] data, string fileName)
    {
        int width = data.Length;
        int height = data[0].Length;

        byte[] imgData = new byte[width * height * 4];
        GCHandle imgGcHandle = GCHandle.Alloc(imgData, GCHandleType.Pinned);
        using Bitmap bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, imgGcHandle.AddrOfPinnedObject());

        double maxValue = double.NegativeInfinity;
        double minValue = double.PositiveInfinity;

        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
                double value = data[ix][iy];

                if (value > maxValue)
                {
                    maxValue = value;
                }

                if (value < minValue)
                {
                    minValue = value;
                }

                byte color = (byte)(Math.Max(0, Math.Min(255, value * 255)));
                int index = (ix + iy * width) * 4;
                imgData[index + 0] = color;
                imgData[index + 1] = color;
                imgData[index + 2] = color;
                imgData[index + 3] = 255;
            }
        }

        Console.WriteLine($"Max: {maxValue} Min: {minValue}");

        bmp.Save(fileName, ImageFormat.Png);
        imgGcHandle.Free();
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double Interpolate(double t, double x0, double x1, double x2, double x3)
    {
        double t2 = t * t;
        double t3 = t2 * t;

        double p = (x3 - x2) - (x0 - x1);
        double q = x0 - x1 - p;
        double r = x2 - x0;
        double s = x1;

        double result = (p * t3) + (q * t2) + (r * t) + s;

        return result;
    }
}