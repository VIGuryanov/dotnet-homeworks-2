using Hw11.Services.MathCalculator.Token;
using Hw11.Services.MathCalculator.Parser;
using Hw11.Exceptions;
using Xunit;

namespace Hw11.Tests
{
    public class MathCalculatorExceptionTests
    {
        [Fact]
        public static void ExpressionPostfixModelParserThrowsArgumentExceptionOnUnexpectedToken()
        {
            Assert.Throws<InvalidSymbolException>(() => ExpressionPostfixModelParser.Parse(new[] { new TestToken() }));
        }
    }

    class TestToken : IToken { }
}
