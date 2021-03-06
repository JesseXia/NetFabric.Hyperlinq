using NetFabric.Assertive;
using System;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests.Projection.Select
{
    public class ValueEnumerableTests
    {
        [Fact]
        public void Select_With_NullSelector_Must_Throw()
        {
            // Arrange
            var enumerable = Wrap.AsValueEnumerable(new int[0]);
            var selector = (NullableSelector<int, string>)null;

            // Act
            Action action = () => _ = ValueEnumerableExtensions.Select<Wrap.ValueEnumerableWrapper<int>, Wrap.Enumerator<int>, int, string>(enumerable, selector);

            // Assert
            _ = action.Must()
                .Throw<ArgumentNullException>()
                .EvaluateTrue(exception => exception.ParamName == "selector");
        }

        [Theory]
        [MemberData(nameof(TestData.SelectorEmpty), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SelectorSingle), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SelectorMultiple), MemberType = typeof(TestData))]
        public void Select_With_ValidData_Must_Succeed(int[] source, NullableSelector<int, string> selector)
        {
            // Arrange
            var wrapped = Wrap.AsValueEnumerable(source);
            var expected = 
                System.Linq.Enumerable.Select(wrapped, selector.AsFunc());

            // Act
            var result = ValueEnumerableExtensions
                .Select<Wrap.ValueEnumerableWrapper<int>, Wrap.Enumerator<int>, int, string>(wrapped, selector);

            // Assert
            _ = result.Must()
                .BeEnumerableOf<string>()
                .BeEqualTo(expected);
        }
    }
}