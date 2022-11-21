using Hw11.ErrorMessages;
using Hw11.Exceptions;
using Hw11.Services.MathCalculator.Token;

namespace Hw11.Services.MathCalculator.Parser
{
    public class ShuntingYardAlgorithm : IShuntingYardAlgorithm
    {
        private readonly Stack<OperatorToken> operatorsStack;
        private readonly List<IToken> postfixNotation;

        public ShuntingYardAlgorithm()
        {
            operatorsStack = new Stack<OperatorToken>();
            postfixNotation = new List<IToken>();
        }

        public IEnumerable<IToken> Apply(IEnumerable<IToken> infixNotation)
        {
            foreach (var token in infixNotation)
            {
                ProcessToken(token);
            }
            return GetResult();
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void ProcessToken(IToken token)
        {
            switch (token)
            {
                case OperandToken operandToken:
                    postfixNotation.Add(operandToken);
                    break;
                case OperatorToken operatorToken:
                    ProcessOperator(operatorToken);
                    break;
                default:
                    throw new ArgumentException($"An unknown token type: {token.GetType()}.");
            }
        }

        private void ProcessOperator(OperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case OperatorType.OpeningBracket:
                    operatorsStack.Push(operatorToken);
                    break;
                case OperatorType.ClosingBracket:
                    PushClosingBracket();
                    break;
                default:
                    PushOperator(operatorToken);
                    break;
            }
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void PushClosingBracket()
        {
            bool openingBracketFound = false;

            while (operatorsStack.Count > 0)
            {
                var stackOperatorToken = operatorsStack.Pop();
                if (stackOperatorToken.Operator == OperatorType.OpeningBracket)
                {
                    openingBracketFound = true;
                    break;
                }

                postfixNotation.Add(stackOperatorToken);
            }

            if (!openingBracketFound)
                throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
        }

        private void PushOperator(OperatorToken operatorToken)
        {
            var operatorPriority = GetOperatorPriority(operatorToken);

            while (operatorsStack.Count > 0)
            {
                var stackOperatorToken = operatorsStack.Peek();
                if (stackOperatorToken.Operator == OperatorType.OpeningBracket)
                {
                    break;
                }

                var stackOperatorPriority = GetOperatorPriority(stackOperatorToken);
                if (stackOperatorPriority < operatorPriority)
                {
                    break;
                }

                postfixNotation.Add(operatorsStack.Pop());
            }

            operatorsStack.Push(operatorToken);
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static int GetOperatorPriority(OperatorToken operatorToken) =>
            operatorToken.Operator switch
            {
                OperatorType.Addition or OperatorType.Subtraction => 1,
                OperatorType.Multiplication or OperatorType.Division => 2,
                _ => throw new InvalidSymbolException($"An unexpected action for the operator: {operatorToken.Operator}."),
            };

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private List<IToken> GetResult()
        {
            while (operatorsStack.Count > 0)
            {
                var token = operatorsStack.Pop();
                if (token.Operator == OperatorType.OpeningBracket)
                {
                    throw new Exception("A redundant opening bracket.");
                }
                postfixNotation.Add(token);
            }
            return postfixNotation.ToList();
        }
    }
}
