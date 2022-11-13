using Hw10.Services.MathCalculator.Token;
using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.Parser
{
    public static class ExpressionPostfixModelParser
    {
        public static Expression Parse(IEnumerable<IToken> input)
        {
            var stack = new Stack<Expression>();
            Expression v1;
            Expression v2;

            foreach (var token in input)
            {
                if (token is OperandToken operandToken)
                    stack.Push(Expression.Constant(operandToken.Value));
                else if (token is OperatorToken operatorToken)
                {
                    v2 = stack.Pop();
                    v1 = stack.Pop();
                    var op = operatorToken.Operator;
                    switch (op)
                    {
                        case OperatorType.Addition:
                            stack.Push(Expression.Add(v1, v2));
                            break;
                        case OperatorType.Subtraction:
                            stack.Push(Expression.Subtract(v1, v2));
                            break;
                        case OperatorType.Multiplication:
                            stack.Push(Expression.Multiply(v1, v2));
                            break;
                        case OperatorType.Division:
                            stack.Push(Expression.Divide(v1, v2));
                            break;
                    }
                }
                else
                    throw new ArgumentException($"Unexpected token of type {token.GetType()} given. Supported only OperatorToken and OperandToken");
            }
            return stack.Pop();
        }
    }
}
