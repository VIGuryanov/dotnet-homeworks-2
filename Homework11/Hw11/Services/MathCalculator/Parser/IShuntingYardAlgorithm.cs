using Hw11.Services.MathCalculator.Token;

namespace Hw11.Services.MathCalculator.Parser
{
    public interface IShuntingYardAlgorithm
    {
        public IEnumerable<IToken> Apply(IEnumerable<IToken> infixNotation);
    }
}
