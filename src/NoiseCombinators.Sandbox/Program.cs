using NoiseCombinators.Hashing;
using NoiseCombinators.NoiseGenerators;
using NoiseCombinators.NoiseGenerators.Basis;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace NoiseCombinators.Sandbox;

public static class Program
{
    public static void Main()
    {
        if (OperatingSystem.IsWindows())
        {
            //SimpleExample();
            BiomeExample();
        }
        else
        {
            Console.WriteLine("Unable to create image on OS that is not Windows.");
        }
    }

    [SupportedOSPlatform("windows")]
    private static void BiomeExample()
    {
        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 125;
        INoise noise = new BicubicNoise()
            .Scale(scale)
            .Shift(-sizeHalf, -sizeHalf)
            .Normalize()
            .ApplySigmoid(4, 8);
        INoise temperatureNoise = noise.Scale(2).WithSeed(42)
            .Add(noise.Scale(1).WithSeed(49).Multiply(0.5))
            .Add(noise.Scale(0.5).WithSeed(50).Multiply(0.25))
            .Add(noise.Scale(0.25).WithSeed(51).Multiply(0.125))
            .Normalize()
            .ApplySigmoid(2, 2);
        INoise humidityNoise = noise.Scale(2).WithSeed(43)
            .Add(noise.Scale(1).WithSeed(47).Multiply(0.5))
            .Add(noise.Scale(0.5).WithSeed(48).Multiply(0.25))
            .Add(noise.Scale(0.25).WithSeed(52).Multiply(0.125))
            .Normalize()
            .Apply(x => x * x, 0, 1)
            .ApplySigmoid(2, 2)
            .Invert();
        INoise heightNoise = noise.Scale(0.4).WithSeed(44)
            .Add(noise.Scale(0.2).WithSeed(45).Multiply(0.5))
            .Add(noise.Scale(0.1).WithSeed(46).Multiply(0.25))
            .Normalize()
            .Apply(x => Math.Pow(x, 1.5), 0, 1)
            .ApplyKernelFilter(Kernels.Gaussian5);
        DateTime time = DateTime.Now;
        double[][] temperatures = temperatureNoise.GetChunk(0, 0, size, size);
        double[][] humidities = humidityNoise.GetChunk(0, 0, size, size);
        double[][] heights = heightNoise.GetChunk(0, 0, size, size);
        SaveAsImage(temperatures, $"{time:yyyy-MM-dd-HH-mm-ss}-heatmap.png", Turbo);
        SaveAsImage(humidities, $"{time:yyyy-MM-dd-HH-mm-ss}-humidity.png", Turbo);
        SaveAsImage(heights, $"{time:yyyy-MM-dd-HH-mm-ss}-height.png", Grayscale);

        byte[][] biomes = new byte[size][];
        Color[] biomeMap = new Color[256];
        Array.Copy(Grayscale, biomeMap, 255);
        biomeMap[1] = Color.Blue; // Ocean
        biomeMap[2] = Color.Cyan; // Frozen Ocean
        biomeMap[3] = Color.SandyBrown; // Beach
        biomeMap[4] = Color.Yellow; // Desert
        biomeMap[5] = Color.ForestGreen; // Forest
        biomeMap[6] = Color.White; // Taiga
        biomeMap[7] = Color.DarkGreen; // Rainforest
        biomeMap[8] = Color.Gray; // Mountains
        biomeMap[9] = Color.White; // Mountain Peak

        double cold = 0.35;
        double warm = 0.6;
        double wet = 0.7;

        for (int x = 0; x < size; x++)
        {
            double[] temperatureCol = temperatures[x];
            double[] humidityCol = humidities[x];
            double[] heightCol = heights[x];
            byte[] col = new byte[size];
            biomes[x] = col;

            for (int y = 0; y < size; y++)
            {
                double temperature = temperatureCol[y];
                double humidity = humidityCol[y];
                double height = heightCol[y];

                byte value;
                if (height < 0.15)
                {
                    if (temperature < cold - 0.05)
                    {
                        value = 2;
                    }
                    else if (temperature < warm || humidity > wet - 0.1)
                    {
                        value = 1;
                    }
                    else
                    {
                        value = 3;
                    }
                }
                else if (height < 0.2)
                {
                    value = 3;
                }
                else if (height < 0.4)
                {
                    if (temperature < cold)
                    {
                        value = 6;
                    }
                    else if (temperature < warm)
                    {
                        value = 5;
                    }
                    else if (humidity > wet)
                    {
                        value = 7;
                    }
                    else
                    {
                        value = 4;
                    }
                }
                else if (height < 0.55)
                {
                    value = 8;
                }
                else if (humidity > wet - 0.075)
                {
                    value = 9;
                }
                else
                {
                    value = 8;
                }

                col[y] = value;
            }
        }

        SaveAsImage(biomes, $"{time:yyyy-MM-dd-HH-mm-ss}-biomes.png", biomeMap);
        SaveAsImage(biomes, $"{time:yyyy-MM-dd-HH-mm-ss}-detail.png", biomeMap, heights);
    }

    [SupportedOSPlatform("windows")]
    private static void SimpleExample()
    {
        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 5;
        INoise noise = new BicubicNoise()
            .WithSeed(42)
            .Scale(scale)
            .Shift(-sizeHalf, -sizeHalf)
            .Normalize()
            .ApplySigmoid(4, 8);
        double[][] data = noise.GetChunk(0, 0, size, size);
        SaveAsImage(data, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png", Turbo);
    }

    [SupportedOSPlatform("windows")]
    private static void SaveAsImage(byte[][] data, string fileName, Color[] colorMap, double[][]? intensities = null)
    {
        int width = data.Length;
        int height = data[0].Length;

        int[] imgData = new int[width * height];
        GCHandle imgGcHandle = GCHandle.Alloc(imgData, GCHandleType.Pinned);
        using Bitmap bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, imgGcHandle.AddrOfPinnedObject());

        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
                byte colorIndex = data[ix][iy];
                Color color = colorMap[colorIndex];

                if (intensities is not null)
                {
                    double h = intensities[ix][iy];
                    double intensity = (0.5 - h) * 255 / 2;
                    byte r = (byte)(Math.Max(0, Math.Min(255, (color.R + intensity) * 0.75)));
                    byte g = (byte)(Math.Max(0, Math.Min(255, (color.G + intensity) * 0.75)));
                    byte b = (byte)(Math.Max(0, Math.Min(255, (color.B + intensity) * 0.75)));
                    color = Color.FromArgb(color.A, r, g, b);
                }

                int index = ix + iy * width;
                imgData[index] = color.ToArgb();
            }
        }

        bmp.Save(fileName, ImageFormat.Png);
        imgGcHandle.Free();
    }

    [SupportedOSPlatform("windows")]
    private static void SaveAsImage(double[][] data, string fileName, Color[] colorMap)
    {
        int width = data.Length;
        int height = data[0].Length;

        int[] imgData = new int[width * height];
        GCHandle imgGcHandle = GCHandle.Alloc(imgData, GCHandleType.Pinned);
        using Bitmap bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, imgGcHandle.AddrOfPinnedObject());

        double maxValue = double.NegativeInfinity;
        double minValue = double.PositiveInfinity;
        double avg = 0;
        int count = 0;

        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
                double value = data[ix][iy];

                if (count == 0)
                {
                    avg = value;
                }
                else
                {
                    avg = (avg * count + value) / (count + 1);
                }

                count++;

                if (value > maxValue)
                {
                    maxValue = value;
                }

                if (value < minValue)
                {
                    minValue = value;
                }

                byte colorIndex = (byte)(Math.Max(0, Math.Min(255, value * 255)));
                Color color = colorMap[colorIndex];
                int index = ix + iy * width;
                imgData[index] = color.ToArgb();
            }
        }

        Console.WriteLine($"Max: {maxValue} Min: {minValue} Avg: {avg}");

        bmp.Save(fileName, ImageFormat.Png);
        imgGcHandle.Free();
    }

    private static Color[] Turbo = new Color[]
    {
        Color.FromArgb(255, 48, 18, 59),
        Color.FromArgb(255, 49, 21, 66),
        Color.FromArgb(255, 50, 24, 74),
        Color.FromArgb(255, 52, 27, 81),
        Color.FromArgb(255, 53, 30, 88),
        Color.FromArgb(255, 54, 33, 95),
        Color.FromArgb(255, 55, 35, 101),
        Color.FromArgb(255, 56, 38, 108),
        Color.FromArgb(255, 57, 41, 114),
        Color.FromArgb(255, 58, 44, 121),
        Color.FromArgb(255, 59, 47, 127),
        Color.FromArgb(255, 60, 50, 133),
        Color.FromArgb(255, 60, 53, 139),
        Color.FromArgb(255, 61, 55, 145),
        Color.FromArgb(255, 62, 58, 150),
        Color.FromArgb(255, 63, 61, 156),
        Color.FromArgb(255, 64, 64, 161),
        Color.FromArgb(255, 64, 67, 166),
        Color.FromArgb(255, 65, 69, 171),
        Color.FromArgb(255, 65, 72, 176),
        Color.FromArgb(255, 66, 75, 181),
        Color.FromArgb(255, 67, 78, 186),
        Color.FromArgb(255, 67, 80, 190),
        Color.FromArgb(255, 67, 83, 194),
        Color.FromArgb(255, 68, 86, 199),
        Color.FromArgb(255, 68, 88, 203),
        Color.FromArgb(255, 69, 91, 206),
        Color.FromArgb(255, 69, 94, 210),
        Color.FromArgb(255, 69, 96, 214),
        Color.FromArgb(255, 69, 99, 217),
        Color.FromArgb(255, 70, 102, 221),
        Color.FromArgb(255, 70, 104, 224),
        Color.FromArgb(255, 70, 107, 227),
        Color.FromArgb(255, 70, 109, 230),
        Color.FromArgb(255, 70, 112, 232),
        Color.FromArgb(255, 70, 115, 235),
        Color.FromArgb(255, 70, 117, 237),
        Color.FromArgb(255, 70, 120, 240),
        Color.FromArgb(255, 70, 122, 242),
        Color.FromArgb(255, 70, 125, 244),
        Color.FromArgb(255, 70, 127, 246),
        Color.FromArgb(255, 70, 130, 248),
        Color.FromArgb(255, 69, 132, 249),
        Color.FromArgb(255, 69, 135, 251),
        Color.FromArgb(255, 69, 137, 252),
        Color.FromArgb(255, 68, 140, 253),
        Color.FromArgb(255, 67, 142, 253),
        Color.FromArgb(255, 66, 145, 254),
        Color.FromArgb(255, 65, 147, 254),
        Color.FromArgb(255, 64, 150, 254),
        Color.FromArgb(255, 63, 152, 254),
        Color.FromArgb(255, 62, 155, 254),
        Color.FromArgb(255, 60, 157, 253),
        Color.FromArgb(255, 59, 160, 252),
        Color.FromArgb(255, 57, 162, 252),
        Color.FromArgb(255, 56, 165, 251),
        Color.FromArgb(255, 54, 168, 249),
        Color.FromArgb(255, 52, 170, 248),
        Color.FromArgb(255, 51, 172, 246),
        Color.FromArgb(255, 49, 175, 245),
        Color.FromArgb(255, 47, 177, 243),
        Color.FromArgb(255, 45, 180, 241),
        Color.FromArgb(255, 43, 182, 239),
        Color.FromArgb(255, 42, 185, 237),
        Color.FromArgb(255, 40, 187, 235),
        Color.FromArgb(255, 38, 189, 233),
        Color.FromArgb(255, 37, 192, 230),
        Color.FromArgb(255, 35, 194, 228),
        Color.FromArgb(255, 33, 196, 225),
        Color.FromArgb(255, 32, 198, 223),
        Color.FromArgb(255, 30, 201, 220),
        Color.FromArgb(255, 29, 203, 218),
        Color.FromArgb(255, 28, 205, 215),
        Color.FromArgb(255, 27, 207, 212),
        Color.FromArgb(255, 26, 209, 210),
        Color.FromArgb(255, 25, 211, 207),
        Color.FromArgb(255, 24, 213, 204),
        Color.FromArgb(255, 24, 215, 202),
        Color.FromArgb(255, 23, 217, 199),
        Color.FromArgb(255, 23, 218, 196),
        Color.FromArgb(255, 23, 220, 194),
        Color.FromArgb(255, 23, 222, 191),
        Color.FromArgb(255, 24, 224, 189),
        Color.FromArgb(255, 24, 225, 186),
        Color.FromArgb(255, 25, 227, 184),
        Color.FromArgb(255, 26, 228, 182),
        Color.FromArgb(255, 27, 229, 180),
        Color.FromArgb(255, 29, 231, 177),
        Color.FromArgb(255, 30, 232, 175),
        Color.FromArgb(255, 32, 233, 172),
        Color.FromArgb(255, 34, 235, 169),
        Color.FromArgb(255, 36, 236, 166),
        Color.FromArgb(255, 39, 237, 163),
        Color.FromArgb(255, 41, 238, 160),
        Color.FromArgb(255, 44, 239, 157),
        Color.FromArgb(255, 47, 240, 154),
        Color.FromArgb(255, 50, 241, 151),
        Color.FromArgb(255, 53, 243, 148),
        Color.FromArgb(255, 56, 244, 145),
        Color.FromArgb(255, 59, 244, 141),
        Color.FromArgb(255, 63, 245, 138),
        Color.FromArgb(255, 66, 246, 135),
        Color.FromArgb(255, 70, 247, 131),
        Color.FromArgb(255, 74, 248, 128),
        Color.FromArgb(255, 77, 249, 124),
        Color.FromArgb(255, 81, 249, 121),
        Color.FromArgb(255, 85, 250, 118),
        Color.FromArgb(255, 89, 251, 114),
        Color.FromArgb(255, 93, 251, 111),
        Color.FromArgb(255, 97, 252, 108),
        Color.FromArgb(255, 101, 252, 104),
        Color.FromArgb(255, 105, 253, 101),
        Color.FromArgb(255, 109, 253, 98),
        Color.FromArgb(255, 113, 253, 95),
        Color.FromArgb(255, 116, 254, 92),
        Color.FromArgb(255, 120, 254, 89),
        Color.FromArgb(255, 124, 254, 86),
        Color.FromArgb(255, 128, 254, 83),
        Color.FromArgb(255, 132, 254, 80),
        Color.FromArgb(255, 135, 254, 77),
        Color.FromArgb(255, 139, 254, 75),
        Color.FromArgb(255, 142, 254, 72),
        Color.FromArgb(255, 146, 254, 70),
        Color.FromArgb(255, 149, 254, 68),
        Color.FromArgb(255, 152, 254, 66),
        Color.FromArgb(255, 155, 253, 64),
        Color.FromArgb(255, 158, 253, 62),
        Color.FromArgb(255, 161, 252, 61),
        Color.FromArgb(255, 164, 252, 59),
        Color.FromArgb(255, 166, 251, 58),
        Color.FromArgb(255, 169, 251, 57),
        Color.FromArgb(255, 172, 250, 55),
        Color.FromArgb(255, 174, 249, 55),
        Color.FromArgb(255, 177, 248, 54),
        Color.FromArgb(255, 179, 248, 53),
        Color.FromArgb(255, 182, 247, 53),
        Color.FromArgb(255, 185, 245, 52),
        Color.FromArgb(255, 187, 244, 52),
        Color.FromArgb(255, 190, 243, 52),
        Color.FromArgb(255, 192, 242, 51),
        Color.FromArgb(255, 195, 241, 51),
        Color.FromArgb(255, 197, 239, 51),
        Color.FromArgb(255, 200, 238, 51),
        Color.FromArgb(255, 202, 237, 51),
        Color.FromArgb(255, 205, 235, 52),
        Color.FromArgb(255, 207, 234, 52),
        Color.FromArgb(255, 209, 232, 52),
        Color.FromArgb(255, 212, 231, 53),
        Color.FromArgb(255, 214, 229, 53),
        Color.FromArgb(255, 216, 227, 53),
        Color.FromArgb(255, 218, 226, 54),
        Color.FromArgb(255, 221, 224, 54),
        Color.FromArgb(255, 223, 222, 54),
        Color.FromArgb(255, 225, 220, 55),
        Color.FromArgb(255, 227, 218, 55),
        Color.FromArgb(255, 229, 216, 56),
        Color.FromArgb(255, 231, 215, 56),
        Color.FromArgb(255, 232, 213, 56),
        Color.FromArgb(255, 234, 211, 57),
        Color.FromArgb(255, 236, 209, 57),
        Color.FromArgb(255, 237, 207, 57),
        Color.FromArgb(255, 239, 205, 57),
        Color.FromArgb(255, 240, 203, 58),
        Color.FromArgb(255, 242, 200, 58),
        Color.FromArgb(255, 243, 198, 58),
        Color.FromArgb(255, 244, 196, 58),
        Color.FromArgb(255, 246, 194, 58),
        Color.FromArgb(255, 247, 192, 57),
        Color.FromArgb(255, 248, 190, 57),
        Color.FromArgb(255, 249, 188, 57),
        Color.FromArgb(255, 249, 186, 56),
        Color.FromArgb(255, 250, 183, 55),
        Color.FromArgb(255, 251, 181, 55),
        Color.FromArgb(255, 251, 179, 54),
        Color.FromArgb(255, 252, 176, 53),
        Color.FromArgb(255, 252, 174, 52),
        Color.FromArgb(255, 253, 171, 51),
        Color.FromArgb(255, 253, 169, 50),
        Color.FromArgb(255, 253, 166, 49),
        Color.FromArgb(255, 253, 163, 48),
        Color.FromArgb(255, 254, 161, 47),
        Color.FromArgb(255, 254, 158, 46),
        Color.FromArgb(255, 254, 155, 45),
        Color.FromArgb(255, 254, 152, 44),
        Color.FromArgb(255, 253, 149, 43),
        Color.FromArgb(255, 253, 146, 41),
        Color.FromArgb(255, 253, 143, 40),
        Color.FromArgb(255, 253, 140, 39),
        Color.FromArgb(255, 252, 137, 38),
        Color.FromArgb(255, 252, 134, 36),
        Color.FromArgb(255, 251, 131, 35),
        Color.FromArgb(255, 251, 128, 34),
        Color.FromArgb(255, 250, 125, 32),
        Color.FromArgb(255, 250, 122, 31),
        Color.FromArgb(255, 249, 119, 30),
        Color.FromArgb(255, 248, 116, 28),
        Color.FromArgb(255, 247, 113, 27),
        Color.FromArgb(255, 247, 110, 26),
        Color.FromArgb(255, 246, 107, 24),
        Color.FromArgb(255, 245, 104, 23),
        Color.FromArgb(255, 244, 101, 22),
        Color.FromArgb(255, 243, 99, 21),
        Color.FromArgb(255, 242, 96, 20),
        Color.FromArgb(255, 241, 93, 19),
        Color.FromArgb(255, 239, 90, 17),
        Color.FromArgb(255, 238, 88, 16),
        Color.FromArgb(255, 237, 85, 15),
        Color.FromArgb(255, 236, 82, 14),
        Color.FromArgb(255, 234, 80, 13),
        Color.FromArgb(255, 233, 77, 13),
        Color.FromArgb(255, 232, 75, 12),
        Color.FromArgb(255, 230, 73, 11),
        Color.FromArgb(255, 229, 70, 10),
        Color.FromArgb(255, 227, 68, 10),
        Color.FromArgb(255, 226, 66, 9),
        Color.FromArgb(255, 224, 64, 8),
        Color.FromArgb(255, 222, 62, 8),
        Color.FromArgb(255, 221, 60, 7),
        Color.FromArgb(255, 219, 58, 7),
        Color.FromArgb(255, 217, 56, 6),
        Color.FromArgb(255, 215, 54, 6),
        Color.FromArgb(255, 214, 52, 5),
        Color.FromArgb(255, 212, 50, 5),
        Color.FromArgb(255, 210, 48, 5),
        Color.FromArgb(255, 208, 47, 4),
        Color.FromArgb(255, 206, 45, 4),
        Color.FromArgb(255, 203, 43, 3),
        Color.FromArgb(255, 201, 41, 3),
        Color.FromArgb(255, 199, 40, 3),
        Color.FromArgb(255, 197, 38, 2),
        Color.FromArgb(255, 195, 36, 2),
        Color.FromArgb(255, 192, 35, 2),
        Color.FromArgb(255, 190, 33, 2),
        Color.FromArgb(255, 187, 31, 1),
        Color.FromArgb(255, 185, 30, 1),
        Color.FromArgb(255, 182, 28, 1),
        Color.FromArgb(255, 180, 27, 1),
        Color.FromArgb(255, 177, 25, 1),
        Color.FromArgb(255, 174, 24, 1),
        Color.FromArgb(255, 172, 22, 1),
        Color.FromArgb(255, 169, 21, 1),
        Color.FromArgb(255, 166, 20, 1),
        Color.FromArgb(255, 163, 18, 1),
        Color.FromArgb(255, 160, 17, 1),
        Color.FromArgb(255, 157, 16, 1),
        Color.FromArgb(255, 154, 14, 1),
        Color.FromArgb(255, 151, 13, 1),
        Color.FromArgb(255, 148, 12, 1),
        Color.FromArgb(255, 145, 11, 1),
        Color.FromArgb(255, 142, 10, 1),
        Color.FromArgb(255, 139, 9, 1),
        Color.FromArgb(255, 135, 8, 1),
        Color.FromArgb(255, 132, 7, 1),
        Color.FromArgb(255, 129, 6, 2),
        Color.FromArgb(255, 125, 5, 2),
        Color.FromArgb(255, 122, 4, 2),
    };

    private static Color[] Grayscale = new Color[]
    {
        Color.FromArgb(255, 0, 0, 0),
        Color.FromArgb(255, 1, 1, 1),
        Color.FromArgb(255, 2, 2, 2),
        Color.FromArgb(255, 3, 3, 3),
        Color.FromArgb(255, 4, 4, 4),
        Color.FromArgb(255, 5, 5, 5),
        Color.FromArgb(255, 6, 6, 6),
        Color.FromArgb(255, 7, 7, 7),
        Color.FromArgb(255, 8, 8, 8),
        Color.FromArgb(255, 9, 9, 9),
        Color.FromArgb(255, 10, 10, 10),
        Color.FromArgb(255, 11, 11, 11),
        Color.FromArgb(255, 12, 12, 12),
        Color.FromArgb(255, 13, 13, 13),
        Color.FromArgb(255, 14, 14, 14),
        Color.FromArgb(255, 15, 15, 15),
        Color.FromArgb(255, 16, 16, 16),
        Color.FromArgb(255, 17, 17, 17),
        Color.FromArgb(255, 18, 18, 18),
        Color.FromArgb(255, 19, 19, 19),
        Color.FromArgb(255, 20, 20, 20),
        Color.FromArgb(255, 21, 21, 21),
        Color.FromArgb(255, 22, 22, 22),
        Color.FromArgb(255, 23, 23, 23),
        Color.FromArgb(255, 24, 24, 24),
        Color.FromArgb(255, 25, 25, 25),
        Color.FromArgb(255, 26, 26, 26),
        Color.FromArgb(255, 27, 27, 27),
        Color.FromArgb(255, 28, 28, 28),
        Color.FromArgb(255, 29, 29, 29),
        Color.FromArgb(255, 30, 30, 30),
        Color.FromArgb(255, 31, 31, 31),
        Color.FromArgb(255, 32, 32, 32),
        Color.FromArgb(255, 33, 33, 33),
        Color.FromArgb(255, 34, 34, 34),
        Color.FromArgb(255, 35, 35, 35),
        Color.FromArgb(255, 36, 36, 36),
        Color.FromArgb(255, 37, 37, 37),
        Color.FromArgb(255, 38, 38, 38),
        Color.FromArgb(255, 39, 39, 39),
        Color.FromArgb(255, 40, 40, 40),
        Color.FromArgb(255, 41, 41, 41),
        Color.FromArgb(255, 42, 42, 42),
        Color.FromArgb(255, 43, 43, 43),
        Color.FromArgb(255, 44, 44, 44),
        Color.FromArgb(255, 45, 45, 45),
        Color.FromArgb(255, 46, 46, 46),
        Color.FromArgb(255, 47, 47, 47),
        Color.FromArgb(255, 48, 48, 48),
        Color.FromArgb(255, 49, 49, 49),
        Color.FromArgb(255, 50, 50, 50),
        Color.FromArgb(255, 51, 51, 51),
        Color.FromArgb(255, 52, 52, 52),
        Color.FromArgb(255, 53, 53, 53),
        Color.FromArgb(255, 54, 54, 54),
        Color.FromArgb(255, 55, 55, 55),
        Color.FromArgb(255, 56, 56, 56),
        Color.FromArgb(255, 57, 57, 57),
        Color.FromArgb(255, 58, 58, 58),
        Color.FromArgb(255, 59, 59, 59),
        Color.FromArgb(255, 60, 60, 60),
        Color.FromArgb(255, 61, 61, 61),
        Color.FromArgb(255, 62, 62, 62),
        Color.FromArgb(255, 63, 63, 63),
        Color.FromArgb(255, 64, 64, 64),
        Color.FromArgb(255, 65, 65, 65),
        Color.FromArgb(255, 66, 66, 66),
        Color.FromArgb(255, 67, 67, 67),
        Color.FromArgb(255, 68, 68, 68),
        Color.FromArgb(255, 69, 69, 69),
        Color.FromArgb(255, 70, 70, 70),
        Color.FromArgb(255, 71, 71, 71),
        Color.FromArgb(255, 72, 72, 72),
        Color.FromArgb(255, 73, 73, 73),
        Color.FromArgb(255, 74, 74, 74),
        Color.FromArgb(255, 75, 75, 75),
        Color.FromArgb(255, 76, 76, 76),
        Color.FromArgb(255, 77, 77, 77),
        Color.FromArgb(255, 78, 78, 78),
        Color.FromArgb(255, 79, 79, 79),
        Color.FromArgb(255, 80, 80, 80),
        Color.FromArgb(255, 81, 81, 81),
        Color.FromArgb(255, 82, 82, 82),
        Color.FromArgb(255, 83, 83, 83),
        Color.FromArgb(255, 84, 84, 84),
        Color.FromArgb(255, 85, 85, 85),
        Color.FromArgb(255, 86, 86, 86),
        Color.FromArgb(255, 87, 87, 87),
        Color.FromArgb(255, 88, 88, 88),
        Color.FromArgb(255, 89, 89, 89),
        Color.FromArgb(255, 90, 90, 90),
        Color.FromArgb(255, 91, 91, 91),
        Color.FromArgb(255, 92, 92, 92),
        Color.FromArgb(255, 93, 93, 93),
        Color.FromArgb(255, 94, 94, 94),
        Color.FromArgb(255, 95, 95, 95),
        Color.FromArgb(255, 96, 96, 96),
        Color.FromArgb(255, 97, 97, 97),
        Color.FromArgb(255, 98, 98, 98),
        Color.FromArgb(255, 99, 99, 99),
        Color.FromArgb(255, 100, 100, 100),
        Color.FromArgb(255, 101, 101, 101),
        Color.FromArgb(255, 102, 102, 102),
        Color.FromArgb(255, 103, 103, 103),
        Color.FromArgb(255, 104, 104, 104),
        Color.FromArgb(255, 105, 105, 105),
        Color.FromArgb(255, 106, 106, 106),
        Color.FromArgb(255, 107, 107, 107),
        Color.FromArgb(255, 108, 108, 108),
        Color.FromArgb(255, 109, 109, 109),
        Color.FromArgb(255, 110, 110, 110),
        Color.FromArgb(255, 111, 111, 111),
        Color.FromArgb(255, 112, 112, 112),
        Color.FromArgb(255, 113, 113, 113),
        Color.FromArgb(255, 114, 114, 114),
        Color.FromArgb(255, 115, 115, 115),
        Color.FromArgb(255, 116, 116, 116),
        Color.FromArgb(255, 117, 117, 117),
        Color.FromArgb(255, 118, 118, 118),
        Color.FromArgb(255, 119, 119, 119),
        Color.FromArgb(255, 120, 120, 120),
        Color.FromArgb(255, 121, 121, 121),
        Color.FromArgb(255, 122, 122, 122),
        Color.FromArgb(255, 123, 123, 123),
        Color.FromArgb(255, 124, 124, 124),
        Color.FromArgb(255, 125, 125, 125),
        Color.FromArgb(255, 126, 126, 126),
        Color.FromArgb(255, 127, 127, 127),
        Color.FromArgb(255, 128, 128, 128),
        Color.FromArgb(255, 129, 129, 129),
        Color.FromArgb(255, 130, 130, 130),
        Color.FromArgb(255, 131, 131, 131),
        Color.FromArgb(255, 132, 132, 132),
        Color.FromArgb(255, 133, 133, 133),
        Color.FromArgb(255, 134, 134, 134),
        Color.FromArgb(255, 135, 135, 135),
        Color.FromArgb(255, 136, 136, 136),
        Color.FromArgb(255, 137, 137, 137),
        Color.FromArgb(255, 138, 138, 138),
        Color.FromArgb(255, 139, 139, 139),
        Color.FromArgb(255, 140, 140, 140),
        Color.FromArgb(255, 141, 141, 141),
        Color.FromArgb(255, 142, 142, 142),
        Color.FromArgb(255, 143, 143, 143),
        Color.FromArgb(255, 144, 144, 144),
        Color.FromArgb(255, 145, 145, 145),
        Color.FromArgb(255, 146, 146, 146),
        Color.FromArgb(255, 147, 147, 147),
        Color.FromArgb(255, 148, 148, 148),
        Color.FromArgb(255, 149, 149, 149),
        Color.FromArgb(255, 150, 150, 150),
        Color.FromArgb(255, 151, 151, 151),
        Color.FromArgb(255, 152, 152, 152),
        Color.FromArgb(255, 153, 153, 153),
        Color.FromArgb(255, 154, 154, 154),
        Color.FromArgb(255, 155, 155, 155),
        Color.FromArgb(255, 156, 156, 156),
        Color.FromArgb(255, 157, 157, 157),
        Color.FromArgb(255, 158, 158, 158),
        Color.FromArgb(255, 159, 159, 159),
        Color.FromArgb(255, 160, 160, 160),
        Color.FromArgb(255, 161, 161, 161),
        Color.FromArgb(255, 162, 162, 162),
        Color.FromArgb(255, 163, 163, 163),
        Color.FromArgb(255, 164, 164, 164),
        Color.FromArgb(255, 165, 165, 165),
        Color.FromArgb(255, 166, 166, 166),
        Color.FromArgb(255, 167, 167, 167),
        Color.FromArgb(255, 168, 168, 168),
        Color.FromArgb(255, 169, 169, 169),
        Color.FromArgb(255, 170, 170, 170),
        Color.FromArgb(255, 171, 171, 171),
        Color.FromArgb(255, 172, 172, 172),
        Color.FromArgb(255, 173, 173, 173),
        Color.FromArgb(255, 174, 174, 174),
        Color.FromArgb(255, 175, 175, 175),
        Color.FromArgb(255, 176, 176, 176),
        Color.FromArgb(255, 177, 177, 177),
        Color.FromArgb(255, 178, 178, 178),
        Color.FromArgb(255, 179, 179, 179),
        Color.FromArgb(255, 180, 180, 180),
        Color.FromArgb(255, 181, 181, 181),
        Color.FromArgb(255, 182, 182, 182),
        Color.FromArgb(255, 183, 183, 183),
        Color.FromArgb(255, 184, 184, 184),
        Color.FromArgb(255, 185, 185, 185),
        Color.FromArgb(255, 186, 186, 186),
        Color.FromArgb(255, 187, 187, 187),
        Color.FromArgb(255, 188, 188, 188),
        Color.FromArgb(255, 189, 189, 189),
        Color.FromArgb(255, 190, 190, 190),
        Color.FromArgb(255, 191, 191, 191),
        Color.FromArgb(255, 192, 192, 192),
        Color.FromArgb(255, 193, 193, 193),
        Color.FromArgb(255, 194, 194, 194),
        Color.FromArgb(255, 195, 195, 195),
        Color.FromArgb(255, 196, 196, 196),
        Color.FromArgb(255, 197, 197, 197),
        Color.FromArgb(255, 198, 198, 198),
        Color.FromArgb(255, 199, 199, 199),
        Color.FromArgb(255, 200, 200, 200),
        Color.FromArgb(255, 201, 201, 201),
        Color.FromArgb(255, 202, 202, 202),
        Color.FromArgb(255, 203, 203, 203),
        Color.FromArgb(255, 204, 204, 204),
        Color.FromArgb(255, 205, 205, 205),
        Color.FromArgb(255, 206, 206, 206),
        Color.FromArgb(255, 207, 207, 207),
        Color.FromArgb(255, 208, 208, 208),
        Color.FromArgb(255, 209, 209, 209),
        Color.FromArgb(255, 210, 210, 210),
        Color.FromArgb(255, 211, 211, 211),
        Color.FromArgb(255, 212, 212, 212),
        Color.FromArgb(255, 213, 213, 213),
        Color.FromArgb(255, 214, 214, 214),
        Color.FromArgb(255, 215, 215, 215),
        Color.FromArgb(255, 216, 216, 216),
        Color.FromArgb(255, 217, 217, 217),
        Color.FromArgb(255, 218, 218, 218),
        Color.FromArgb(255, 219, 219, 219),
        Color.FromArgb(255, 220, 220, 220),
        Color.FromArgb(255, 221, 221, 221),
        Color.FromArgb(255, 222, 222, 222),
        Color.FromArgb(255, 223, 223, 223),
        Color.FromArgb(255, 224, 224, 224),
        Color.FromArgb(255, 225, 225, 225),
        Color.FromArgb(255, 226, 226, 226),
        Color.FromArgb(255, 227, 227, 227),
        Color.FromArgb(255, 228, 228, 228),
        Color.FromArgb(255, 229, 229, 229),
        Color.FromArgb(255, 230, 230, 230),
        Color.FromArgb(255, 231, 231, 231),
        Color.FromArgb(255, 232, 232, 232),
        Color.FromArgb(255, 233, 233, 233),
        Color.FromArgb(255, 234, 234, 234),
        Color.FromArgb(255, 235, 235, 235),
        Color.FromArgb(255, 236, 236, 236),
        Color.FromArgb(255, 237, 237, 237),
        Color.FromArgb(255, 238, 238, 238),
        Color.FromArgb(255, 239, 239, 239),
        Color.FromArgb(255, 240, 240, 240),
        Color.FromArgb(255, 241, 241, 241),
        Color.FromArgb(255, 242, 242, 242),
        Color.FromArgb(255, 243, 243, 243),
        Color.FromArgb(255, 244, 244, 244),
        Color.FromArgb(255, 245, 245, 245),
        Color.FromArgb(255, 246, 246, 246),
        Color.FromArgb(255, 247, 247, 247),
        Color.FromArgb(255, 248, 248, 248),
        Color.FromArgb(255, 249, 249, 249),
        Color.FromArgb(255, 250, 250, 250),
        Color.FromArgb(255, 251, 251, 251),
        Color.FromArgb(255, 252, 252, 252),
        Color.FromArgb(255, 253, 253, 253),
        Color.FromArgb(255, 254, 254, 254),
        Color.FromArgb(255, 255, 255, 255),
    };
}