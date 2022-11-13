using Hw10.Services.MathCalculator.Token;

namespace Hw10.Services.MathCalculator.Parser
{
    public interface IShuntingYardAlgorithm
    {
        public IEnumerable<IToken> Apply(IEnumerable<IToken> infixNotation);
    }
}
