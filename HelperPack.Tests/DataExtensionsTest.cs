using System;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{
    public class DataExtensionsTest
    {
        [Fact]
        public void DataExtensions_Compress_Decompress()
        {
            string sample = "This is a test class for ConversionExtensionsTest and is intended to contain all ConversionExtensionsTest Unit Tests";
            byte[] data = Encoding.UTF8.GetBytes(sample);
            byte[] compressed = DataExtensions.Compress(data);
            byte[] decompressed = DataExtensions.Decompress(compressed);
            string output = Encoding.UTF8.GetString(decompressed);

            decompressed.Should().Equal(data);
            output.Should().Be(sample);
        }

        [Fact]
        public void DataExtensions_ToHexaTest()
        {
            byte[] data = { 0, 1, 15, 255 };
            string expected = "00010FFF";

            DataExtensions.ToHexa(data).Should().Be(expected);
        }

        [Fact]
        public void DataExtensions_ToHexaTestThrowOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => DataExtensions.ToHexa((byte[])null));
        }

        [Fact]
        public void DataExtensions_ToFileSizeThrowsOnNegativeInput()
        {
            Assert.Throws<ArgumentException>(() => DataExtensions.ToFileSize((-1)));
        }

        [Fact]
        public void DataExtensions_ToFileSizeConvertToHumanReadable()
        {
            DataExtensions.ToFileSize(100).Should().Be("100 Bytes");
            DataExtensions.ToFileSize(80530636).Should().Be("76.8 MB");
        }

        enum TestEnum { A, B, C };

        [Fact]
        public void DataExtensions_ToEnumTest()
        {
            DataExtensions.ToEnum<TestEnum>(0).Should().Be(TestEnum.A);
            DataExtensions.ToEnum<TestEnum>(1).Should().Be(TestEnum.B);
            DataExtensions.ToEnum<TestEnum>(2).Should().Be(TestEnum.C);
        }

        [Fact]
        public void DataExtensions_ToEnumThrowOnBadValueTest()
        {
            Assert.Throws<ArgumentException>(() => DataExtensions.ToEnum<TestEnum>((-1)));

        }

        [Fact]
        public void DataExtensions_ToEnumThrowsOnBadTypeTest()
        {
            Assert.Throws<InvalidOperationException>(() => DataExtensions.ToEnum<int>(0));
        }

        [Fact]
        public void FileUtilities_ReadBinaryContent()
        {
            byte[] data = Encoding.UTF8.GetBytes("test data");

            byte[] result;
            using (MemoryStream input = new MemoryStream(data))
            {
                result = FileUtilities.ReadBinaryContent(input);
            }

            result.Should().Equal(data);
        }

    }
}
