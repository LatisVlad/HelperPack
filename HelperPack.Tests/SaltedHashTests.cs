using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{

    public class SaltedHashTests
    {
        private string password = "test";
        private string good = "test";
        private string bad = "bad";

        [Fact]
        public void SaltedHash_CanCreateHash()
        {
            HelperPack.SaltedHash.Generate(password).Should().NotBeEmpty();
        }

        [Fact]
        public void SaltedHash_CanVerifyCreatedHash()
        {
            HelperPack.SaltedHash hash = new HelperPack.SaltedHash();
            string hashed = HelperPack.SaltedHash.Generate(password);

            bool result = hash.VerifyHash(password, hashed);

            result.Should().BeTrue();
        }

        [Fact]
        public void SaltedHash_CanVerifyBadPass()
        {
            HelperPack.SaltedHash hash = new HelperPack.SaltedHash();
            string hashed = HelperPack.SaltedHash.Generate(good);

            bool result = hash.VerifyHash(bad, hashed);

            result.Should().BeFalse();
        }
    }
}
