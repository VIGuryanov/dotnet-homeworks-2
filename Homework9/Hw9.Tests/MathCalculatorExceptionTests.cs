using Hw9.Services.MathCalculator.Token;
using Hw9.Services.MathCalculator.Parser;
using Xunit;

namespace Hw9.Tests
{
    public class MathCalculatorExceptionTests
    {
        [Fact]
        public static void ExpressionPostfixModelParserThrowsArgumentExceptionOnUnexpectedToken()
        {
            Assert.Throws<ArgumentException>(() => new ExpressionPostfixModelParser().Parse(new[] { new TestToken() }));
        }
    }

    class TestToken : IToken { }
}
