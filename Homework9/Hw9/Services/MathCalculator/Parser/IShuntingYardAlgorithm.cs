using Hw9.Services.MathCalculator.Token;

namespace Hw9.Services.MathCalculator.Parser
{
    public interface IShuntingYardAlgorithm
    {
        public IEnumerable<IToken> Apply(IEnumerable<IToken> infixNotation);
    }
}
