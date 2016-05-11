using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HelperPack.Tests
{

    public class QueryExtentionsTests
    {
        private readonly IQueryable<int> data = new List<int> { 1, 2, 3, 4, 5 }.AsQueryable();

        [Fact]
        public void QueryExtension_ApplyIf()
        {
            var result = data.ApplyIf(true, i => i == 1);
            result.Count().Should().Be(1);

            result = data.ApplyIf(false, i => i == 1);
            result.Count().Should().Be(data.Count());
        }

        [Fact]
        public void QueryExtension_ApplyIfNotNull()
        {
            var result = data.ApplyIfNotNull(new object(), i => i == 1);
            result.Count().Should().Be(1);
            object value = null;
            result = data.ApplyIfNotNull(value, i => i == 1);
            result.Count().Should().Be(data.Count());
        }

        [Fact]
        public void QueryExtension_ApplyIfNotEmptyString()
        {
            string value = "asd";

            var result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(1);

            value = null;

            result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(data.Count());
        }

        [Fact]
        public void QueryExtension_ApplyIfNotEmptyGuid()
        {
            Guid value = Guid.NewGuid();

            var result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(1);

            value = Guid.Empty;

            result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(data.Count());
        }

        [Fact]
        public void QueryExtension_ApplyIfNotEmptyDate()
        {
            DateTime value = DateTime.Now;

            var result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(1);

            value = new DateTime();

            result = data.ApplyIfNotEmpty(value, i => i == 1);
            result.Count().Should().Be(data.Count());
        }
    }
}
