using Hw10.Services.MathCalculator.Token;

namespace Hw10.Services.MathCalculator.Parser
{
    public interface IMathExpressionTokenizerParser
    {
        public IEnumerable<IToken> Parse(string expression);
    }
}
