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
        //Console.WriteLine(BuildTurboStr());
        //return;

        const int size = 2560;
        const int sizeHalf = size / 2;
        const double scale = 50;
        INoise noise = new BicubicNoise(42)
            .Scale(scale)
            .Shift(-sizeHalf, -sizeHalf)
            .Normalize()
            .ApplySigmoid(4, 4)
            .ApplyKernelFilter(Kernels.Gaussian5());
        double[][] data = noise.GetChunk(0, 0, size, size);
        SaveAsImage(data, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png");
    }

    private static void SaveAsImage(double[][] data, string fileName)
    {
        int width = data.Length;
        int height = data[0].Length;

        //byte[] imgData = new byte[width * height * 4];
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
                //int index = (ix + iy * width) * 4;
                //imgData[index + 0] = color;
                //imgData[index + 1] = color;
                //imgData[index + 2] = color;
                //imgData[index + 3] = 255;
            }
        }

        Console.WriteLine($"Max: {maxValue} Min: {minValue} Avg: {avg}");

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

    private static string BuildTurboStr()
    {
        string input = "[0.18995,0.07176,0.23217],[0.19483,0.08339,0.26149],[0.19956,0.09498,0.29024],[0.20415,0.10652,0.31844],[0.20860,0.11802,0.34607],[0.21291,0.12947,0.37314],[0.21708,0.14087,0.39964],[0.22111,0.15223,0.42558],[0.22500,0.16354,0.45096],[0.22875,0.17481,0.47578],[0.23236,0.18603,0.50004],[0.23582,0.19720,0.52373],[0.23915,0.20833,0.54686],[0.24234,0.21941,0.56942],[0.24539,0.23044,0.59142],[0.24830,0.24143,0.61286],[0.25107,0.25237,0.63374],[0.25369,0.26327,0.65406],[0.25618,0.27412,0.67381],[0.25853,0.28492,0.69300],[0.26074,0.29568,0.71162],[0.26280,0.30639,0.72968],[0.26473,0.31706,0.74718],[0.26652,0.32768,0.76412],[0.26816,0.33825,0.78050],[0.26967,0.34878,0.79631],[0.27103,0.35926,0.81156],[0.27226,0.36970,0.82624],[0.27334,0.38008,0.84037],[0.27429,0.39043,0.85393],[0.27509,0.40072,0.86692],[0.27576,0.41097,0.87936],[0.27628,0.42118,0.89123],[0.27667,0.43134,0.90254],[0.27691,0.44145,0.91328],[0.27701,0.45152,0.92347],[0.27698,0.46153,0.93309],[0.27680,0.47151,0.94214],[0.27648,0.48144,0.95064],[0.27603,0.49132,0.95857],[0.27543,0.50115,0.96594],[0.27469,0.51094,0.97275],[0.27381,0.52069,0.97899],[0.27273,0.53040,0.98461],[0.27106,0.54015,0.98930],[0.26878,0.54995,0.99303],[0.26592,0.55979,0.99583],[0.26252,0.56967,0.99773],[0.25862,0.57958,0.99876],[0.25425,0.58950,0.99896],[0.24946,0.59943,0.99835],[0.24427,0.60937,0.99697],[0.23874,0.61931,0.99485],[0.23288,0.62923,0.99202],[0.22676,0.63913,0.98851],[0.22039,0.64901,0.98436],[0.21382,0.65886,0.97959],[0.20708,0.66866,0.97423],[0.20021,0.67842,0.96833],[0.19326,0.68812,0.96190],[0.18625,0.69775,0.95498],[0.17923,0.70732,0.94761],[0.17223,0.71680,0.93981],[0.16529,0.72620,0.93161],[0.15844,0.73551,0.92305],[0.15173,0.74472,0.91416],[0.14519,0.75381,0.90496],[0.13886,0.76279,0.89550],[0.13278,0.77165,0.88580],[0.12698,0.78037,0.87590],[0.12151,0.78896,0.86581],[0.11639,0.79740,0.85559],[0.11167,0.80569,0.84525],[0.10738,0.81381,0.83484],[0.10357,0.82177,0.82437],[0.10026,0.82955,0.81389],[0.09750,0.83714,0.80342],[0.09532,0.84455,0.79299],[0.09377,0.85175,0.78264],[0.09287,0.85875,0.77240],[0.09267,0.86554,0.76230],[0.09320,0.87211,0.75237],[0.09451,0.87844,0.74265],[0.09662,0.88454,0.73316],[0.09958,0.89040,0.72393],[0.10342,0.89600,0.71500],[0.10815,0.90142,0.70599],[0.11374,0.90673,0.69651],[0.12014,0.91193,0.68660],[0.12733,0.91701,0.67627],[0.13526,0.92197,0.66556],[0.14391,0.92680,0.65448],[0.15323,0.93151,0.64308],[0.16319,0.93609,0.63137],[0.17377,0.94053,0.61938],[0.18491,0.94484,0.60713],[0.19659,0.94901,0.59466],[0.20877,0.95304,0.58199],[0.22142,0.95692,0.56914],[0.23449,0.96065,0.55614],[0.24797,0.96423,0.54303],[0.26180,0.96765,0.52981],[0.27597,0.97092,0.51653],[0.29042,0.97403,0.50321],[0.30513,0.97697,0.48987],[0.32006,0.97974,0.47654],[0.33517,0.98234,0.46325],[0.35043,0.98477,0.45002],[0.36581,0.98702,0.43688],[0.38127,0.98909,0.42386],[0.39678,0.99098,0.41098],[0.41229,0.99268,0.39826],[0.42778,0.99419,0.38575],[0.44321,0.99551,0.37345],[0.45854,0.99663,0.36140],[0.47375,0.99755,0.34963],[0.48879,0.99828,0.33816],[0.50362,0.99879,0.32701],[0.51822,0.99910,0.31622],[0.53255,0.99919,0.30581],[0.54658,0.99907,0.29581],[0.56026,0.99873,0.28623],[0.57357,0.99817,0.27712],[0.58646,0.99739,0.26849],[0.59891,0.99638,0.26038],[0.61088,0.99514,0.25280],[0.62233,0.99366,0.24579],[0.63323,0.99195,0.23937],[0.64362,0.98999,0.23356],[0.65394,0.98775,0.22835],[0.66428,0.98524,0.22370],[0.67462,0.98246,0.21960],[0.68494,0.97941,0.21602],[0.69525,0.97610,0.21294],[0.70553,0.97255,0.21032],[0.71577,0.96875,0.20815],[0.72596,0.96470,0.20640],[0.73610,0.96043,0.20504],[0.74617,0.95593,0.20406],[0.75617,0.95121,0.20343],[0.76608,0.94627,0.20311],[0.77591,0.94113,0.20310],[0.78563,0.93579,0.20336],[0.79524,0.93025,0.20386],[0.80473,0.92452,0.20459],[0.81410,0.91861,0.20552],[0.82333,0.91253,0.20663],[0.83241,0.90627,0.20788],[0.84133,0.89986,0.20926],[0.85010,0.89328,0.21074],[0.85868,0.88655,0.21230],[0.86709,0.87968,0.21391],[0.87530,0.87267,0.21555],[0.88331,0.86553,0.21719],[0.89112,0.85826,0.21880],[0.89870,0.85087,0.22038],[0.90605,0.84337,0.22188],[0.91317,0.83576,0.22328],[0.92004,0.82806,0.22456],[0.92666,0.82025,0.22570],[0.93301,0.81236,0.22667],[0.93909,0.80439,0.22744],[0.94489,0.79634,0.22800],[0.95039,0.78823,0.22831],[0.95560,0.78005,0.22836],[0.96049,0.77181,0.22811],[0.96507,0.76352,0.22754],[0.96931,0.75519,0.22663],[0.97323,0.74682,0.22536],[0.97679,0.73842,0.22369],[0.98000,0.73000,0.22161],[0.98289,0.72140,0.21918],[0.98549,0.71250,0.21650],[0.98781,0.70330,0.21358],[0.98986,0.69382,0.21043],[0.99163,0.68408,0.20706],[0.99314,0.67408,0.20348],[0.99438,0.66386,0.19971],[0.99535,0.65341,0.19577],[0.99607,0.64277,0.19165],[0.99654,0.63193,0.18738],[0.99675,0.62093,0.18297],[0.99672,0.60977,0.17842],[0.99644,0.59846,0.17376],[0.99593,0.58703,0.16899],[0.99517,0.57549,0.16412],[0.99419,0.56386,0.15918],[0.99297,0.55214,0.15417],[0.99153,0.54036,0.14910],[0.98987,0.52854,0.14398],[0.98799,0.51667,0.13883],[0.98590,0.50479,0.13367],[0.98360,0.49291,0.12849],[0.98108,0.48104,0.12332],[0.97837,0.46920,0.11817],[0.97545,0.45740,0.11305],[0.97234,0.44565,0.10797],[0.96904,0.43399,0.10294],[0.96555,0.42241,0.09798],[0.96187,0.41093,0.09310],[0.95801,0.39958,0.08831],[0.95398,0.38836,0.08362],[0.94977,0.37729,0.07905],[0.94538,0.36638,0.07461],[0.94084,0.35566,0.07031],[0.93612,0.34513,0.06616],[0.93125,0.33482,0.06218],[0.92623,0.32473,0.05837],[0.92105,0.31489,0.05475],[0.91572,0.30530,0.05134],[0.91024,0.29599,0.04814],[0.90463,0.28696,0.04516],[0.89888,0.27824,0.04243],[0.89298,0.26981,0.03993],[0.88691,0.26152,0.03753],[0.88066,0.25334,0.03521],[0.87422,0.24526,0.03297],[0.86760,0.23730,0.03082],[0.86079,0.22945,0.02875],[0.85380,0.22170,0.02677],[0.84662,0.21407,0.02487],[0.83926,0.20654,0.02305],[0.83172,0.19912,0.02131],[0.82399,0.19182,0.01966],[0.81608,0.18462,0.01809],[0.80799,0.17753,0.01660],[0.79971,0.17055,0.01520],[0.79125,0.16368,0.01387],[0.78260,0.15693,0.01264],[0.77377,0.15028,0.01148],[0.76476,0.14374,0.01041],[0.75556,0.13731,0.00942],[0.74617,0.13098,0.00851],[0.73661,0.12477,0.00769],[0.72686,0.11867,0.00695],[0.71692,0.11268,0.00629],[0.70680,0.10680,0.00571],[0.69650,0.10102,0.00522],[0.68602,0.09536,0.00481],[0.67535,0.08980,0.00449],[0.66449,0.08436,0.00424],[0.65345,0.07902,0.00408],[0.64223,0.07380,0.00401],[0.63082,0.06868,0.00401],[0.61923,0.06367,0.00410],[0.60746,0.05878,0.00427],[0.59550,0.05399,0.00453],[0.58336,0.04931,0.00486],[0.57103,0.04474,0.00529],[0.55852,0.04028,0.00579],[0.54583,0.03593,0.00638],[0.53295,0.03169,0.00705],[0.51989,0.02756,0.00780],[0.50664,0.02354,0.00863],[0.49321,0.01963,0.00955],[0.47960,0.01583,0.01055]";
        string[] chunks = input.Split(']');
        var foo = chunks
            .Select(x => x
                .Replace(",[", string.Empty)
                .Replace("[", string.Empty)
                .Split(','))
            .Where(x => x.Length == 3)
            .Select(x =>
            {
                double rd = double.Parse(x[0], CultureInfo.InvariantCulture);
                double gd = double.Parse(x[1], CultureInfo.InvariantCulture);
                double bd = double.Parse(x[2], CultureInfo.InvariantCulture);

                byte r = (byte)(rd * 255);
                byte g = (byte)(gd * 255);
                byte b = (byte)(bd * 255);

                return $"\tColor.FromArgb(255, {r}, {g}, {b}),";
            });


        return string.Join(Environment.NewLine, foo);
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