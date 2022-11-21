using Hw11.Services.MathCalculator.Token;

namespace Hw11.Services.MathCalculator.Parser
{
    public interface IMathExpressionTokenizerParser
    {
        public IEnumerable<IToken> Parse(string expression);
    }
}
