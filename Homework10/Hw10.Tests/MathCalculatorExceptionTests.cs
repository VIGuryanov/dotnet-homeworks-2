using Hw10.Services.MathCalculator.Token;
using Hw10.Services.MathCalculator.Parser;
using Xunit;
using System;

namespace Hw10.Tests
{
    public class MathCalculatorExceptionTests
    {
        [Fact]
        public static void ExpressionPostfixModelParserThrowsArgumentExceptionOnUnexpectedToken()
        {
            Assert.Throws<ArgumentException>(() => ExpressionPostfixModelParser.Parse(new[] { new TestToken() }));
        }
    }

    class TestToken : IToken { }
}
