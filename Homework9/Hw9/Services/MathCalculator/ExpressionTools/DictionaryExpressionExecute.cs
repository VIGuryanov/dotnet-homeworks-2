using Hw9.ErrorMessages;
using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator.ExpressionTools
{
    public static class DictionaryExpressionExecute
    {
        public static async Task<double> ExecuteAsync(Dictionary<Expression, Expression[]> executeBefore)
        {
            var fullExpression = executeBefore.Keys.First();
            var lazy = new Dictionary<Expression, Lazy<Task<double>>>();            

            foreach (var (action, before) in executeBefore)
            {
                lazy[action] = new Lazy<Task<double>>(async () =>
                {
                    if (before.Length > 0)
                    {
                        await Task.WhenAll(before.Select(b => lazy[b].Value));
                        await Task.Yield();

                        await Task.Delay(1000);
                    }

                    if (action is BinaryExpression bin)
                        return Calculate(await lazy[bin.Left].Value,await lazy[bin.Right].Value, bin.NodeType);
                    else
                        return (double)(action as ConstantExpression).Value;
                });
            }

            return await lazy[fullExpression].Value;
        }

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
