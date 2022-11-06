using Hw9.Services.MathCalculator;
using Xunit;

namespace Hw9.Tests
{
    public class OperatorTypeExtensionTests
    {
        [Theory]
        [InlineData(OperatorType.Addition, "+")]
        [InlineData(OperatorType.Subtraction, "-")]
        [InlineData(OperatorType.Multiplication, "*")]
        [InlineData(OperatorType.Division, "/")]
        [InlineData(OperatorType.OpeningBracket, "(")]
        [InlineData(OperatorType.ClosingBracket, ")")]
        public static void ToStringTest(OperatorType opType, string expected)
        {
            var actual = OperatorTypeExtensions.ToString(opType);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void UndefinedThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => OperatorTypeExtensions.ToString(OperatorType.Undefined));
        }
    }
}
