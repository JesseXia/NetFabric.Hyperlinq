using NetFabric.Assertive;
using System;
using System.Linq;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests.Partitioning.Skip
{
    public class ArrayTests
    {
        [Theory]
        [MemberData(nameof(TestData.SkipEmpty), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SkipSingle), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SkipMultiple), MemberType = typeof(TestData))]
        public void Skip_With_ValidData_Must_Succeed(int[] source, int count)
        {
            // Arrange
            var expected = Enumerable
                .Skip(source, count);

            // Act
            var result = ArrayExtensions
                .Skip(source, count);

            // Assert
            _ = result
                .SequenceEqual(expected);
        }
    }
}