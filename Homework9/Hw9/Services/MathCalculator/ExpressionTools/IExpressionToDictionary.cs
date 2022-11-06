using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator.ExpressionTools
{
    public interface IExpressionToDictionary
    {
        public Dictionary<Expression, Expression[]> Convert(Expression expTree);
    }
}
