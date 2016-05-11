using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{
    public class BatchTests
    {
        private static bool EnumerableEquals(IEnumerable<int> x, IEnumerable<int> y)
        {
            return x.Count() == y.Count() && x.Zip(y, (i, j) => i == j).All(b => b);
        }

        [Fact]
        public void Batch_SplitsCollectionIntoExpectedbatches()
        {
            var data = Enumerable.Range(0, 5);

            for (int i = 1; i < 10; i++)
            {
                CollectionsExtensions.Batch(data, i).Sum(b => b.Count()).Should().Be(data.Count());

            }

            Assert.Throws<ArgumentException>(() => CollectionsExtensions.Batch(data, 0).ToArray());
            CollectionsExtensions.Batch(data, 1).Should().Equal(data.Select(d => new[] { d }), (x, y) => EnumerableEquals(x, y));
            CollectionsExtensions.Batch(data, 2).Should().Equal(new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4 } }, (x, y) => EnumerableEquals(x, y));
        }
    }
}
