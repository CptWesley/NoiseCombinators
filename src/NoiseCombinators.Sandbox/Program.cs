using NoiseCombinators.Hashing;
using NoiseCombinators.NoiseGenerators;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace NoiseCombinators.Sandbox;

public static class Program
{
    public static void Main()
    {
        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 0.02;
        INoise noise = new MonotoneBicubicNoise(42);
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

        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
                double value = data[ix][iy];
                byte color = (byte)(Math.Max(0, Math.Min(255, value * 255)));
                int index = (ix + iy * width) * 4;
                imgData[index + 0] = color;
                imgData[index + 1] = color;
                imgData[index + 2] = color;
                imgData[index + 3] = 255;
            }
        }

        bmp.Save(fileName, ImageFormat.Png);
        imgGcHandle.Free();
    }
}