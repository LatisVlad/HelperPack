using System;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{
    
    public class ValueExtensionsTests
    {
        [Fact]
        public void ValueExtensions_GuidIsNullOrEmpty()
        {
            Guid empty = Guid.Empty;
            Guid defaultG = new Guid();
            Guid notEmpty = Guid.NewGuid();

            StructExtensions.IsDefault(empty).Should().BeTrue();
            StructExtensions.IsDefault(defaultG).Should().BeTrue();
            StructExtensions.IsDefault(notEmpty).Should().BeFalse();
        }

        [Fact]
        public void ValueExtensions_DateTimeIsNullOrEmpty()
        {
            DateTime emptyDate = DateTime.MinValue;
            DateTime defaultG = new DateTime();
            DateTime notEmptyDate = DateTime.Now;

            StructExtensions.IsDefault(emptyDate).Should().BeTrue();
            StructExtensions.IsDefault(defaultG).Should().BeTrue();            
            StructExtensions.IsDefault(notEmptyDate).Should().BeFalse();
        }
    }
}
