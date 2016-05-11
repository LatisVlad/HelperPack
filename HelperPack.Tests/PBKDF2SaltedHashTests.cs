using System.Globalization;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{
    public class PBKDF2SaltedHashTests
    {
        [Fact]
        public void GeneratedHashShoudStartAlgorithmPrefix()
        {
            var hash = HelperPack.PBKDF2SaltedHash.GenerateHash("test");
            hash.Should().StartWith("PBKDF2");
        }

        [Fact]
        public void GeneratedHashShoudContainIterationsCount()
        {
            var hash = HelperPack.PBKDF2SaltedHash.GenerateHash("test");
            int algo = "PBKDF2".Length;
            int iteations = int.Parse(hash.Substring(algo, hash.IndexOf(".", algo) - algo), NumberStyles.HexNumber);
            iteations.Should().Be(1000);
        }

        [Fact]
        public void GeneratedHashShoudContainSaltLength()
        {
            var hash = HelperPack.PBKDF2SaltedHash.GenerateHash("test");
            int algo = "PBKDF2".Length;
            int algoAndIterations = hash.IndexOf(".", algo) + 1;
            string saltLengthString = hash.Substring(algoAndIterations, hash.IndexOf(".", algoAndIterations) - algoAndIterations);
            int saltLength = int.Parse(saltLengthString, NumberStyles.HexNumber);
            saltLength.Should().BeGreaterThan(16);
        }

        [Fact]
        public void VerifiesGenertedHash()
        {
            var hash = HelperPack.PBKDF2SaltedHash.GenerateHash("test");
            HelperPack.PBKDF2SaltedHash.VerifyHash("test", hash).Should().BeTrue();
            HelperPack.PBKDF2SaltedHash.VerifyHash("test1", hash).Should().BeFalse();


            var week = HelperPack.SaltedHash.Generate("test");
            var strong = HelperPack.SaltedHash.Generate("test");
        }

    }
}
