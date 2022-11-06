using Hw9.Services.MathCalculator.Token;

namespace Hw9.Services.MathCalculator.Parser
{
    public interface IMathExpressionTokenizerParser
    {
        public IEnumerable<IToken> Parse(string expression);
    }
}
