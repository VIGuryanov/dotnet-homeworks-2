using Hw9.ErrorMessages;
using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator
{
    public class CalculatorExpressionVisitor
    {
        public async Task<double> VisitTree(Expression expression) => await Visit((dynamic)expression);

        public async Task<double> Visit(BinaryExpression expression)
        {
            Thread.Sleep(1000);

            var left = (dynamic)expression.Left;
            var right = (dynamic)expression.Right;
            var lval = 0.0;
            var rval = 0.0;
            var t1 = Task.Run(async () => lval = await Visit(left));
            var t2 = Task.Run(async () => rval = await Visit(right));
            await Task.WhenAll(t1, t2);
            return Calculate(lval, rval, expression.NodeType);
        }

        public async Task<double> Visit(ConstantExpression expression) => (double)expression.Value;

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static double Calculate(double val1, double val2, ExpressionType op) =>
            op switch
            {
                ExpressionType.Add => val1 + val2,
                ExpressionType.Subtract => val1 - val2,
                ExpressionType.Multiply => val1 * val2,
                ExpressionType.Divide => val2 == 0 ? throw new DivideByZeroException(MathErrorMessager.DivisionByZero) : val1 / val2,
                _ => throw new InvalidOperationException($"Unexpected expression type {op.GetType()} given")
            };
    }
}
