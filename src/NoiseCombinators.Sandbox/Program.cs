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

namespace NoiseCombinators.Sandbox;

public static class Program
{
    public static void Main()
    {
        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 50;
        INoise noise = new BicubicNoise(42)
            .Scale(scale)
            .Shift(-sizeHalf, -sizeHalf)
            .Normalize()
            .ApplySigmoid(4, 8);
        double[][] data = noise.GetChunk(0, 0, size, size);
        SaveAsImage(data, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png");
    }

    private static void SaveAsImage(double[][] data, string fileName)
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
                Color color = Turbo[colorIndex];
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
}