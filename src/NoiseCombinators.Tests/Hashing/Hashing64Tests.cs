using NoiseCombinators.Hashing;
using Xunit;
using static AssertNet.Assertions;

namespace NoiseCombinators.Tests.Hashing;

/// <summary>
/// Provides tests for the <see cref="Hashing64"/> class.
/// </summary>
public static class Hashing64Tests
{
    /// <summary>
    /// Checks that the hashing function produces uniformly distributed output.
    /// </summary>
    /// <param name="algorithm">The algorithm under test.</param>
    /// <param name="seed">The seed to use for the testing.</param>
    [Theory]
    [InlineData(Hashing64Algorithm.XXHash, 3424)]
    public static void IsRoughlyUniform(Hashing64Algorithm algorithm, int seed)
    {
        IHashing64 hashing = HashingFactory.Create(algorithm, seed);
        const int bucketCount = 20;
        const int samples = 1000000;
        const float slackRatio = 0.05f;
        int[] buckets = new int[bucketCount];

        for (ulong i = 0; i < samples; i++)
        {
            ulong hash = hashing.HashU64(i);
            float hashF = hash / (float)ulong.MaxValue;
            int index = (int)(hashF * bucketCount);
            buckets[index]++;
        }

        int expected = samples / bucketCount;
        int slack = (int)(slackRatio * expected);
        for (int i = 0; i < bucketCount; i++)
        {
            int count = buckets[i];

            AssertThat(count)
                .IsGreaterThanOrEqualTo(expected - slack)
                .IsLesserThanOrEqualTo(expected + slack);
        }
    }
}
