using Hw11.Services.MathCalculator.ExpressionTools;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Hw11.Services.MathCalculator
{
    public class ExpressionToDictionary : IExpressionToDictionary
    {
        private Dictionary<Expression, Expression[]> executeBefore = new();

        public Dictionary<Expression, Expression[]> Convert(Expression expTree)
        {
            Visit((dynamic)expTree);
            return executeBefore;
        }

        private void Visit(BinaryExpression bin)
        {
            executeBefore.Add(bin, new[] { bin.Left, bin.Right });

            Visit((dynamic)bin.Left);
            Visit((dynamic)bin.Right);
        }

        private void Visit(ConstantExpression con) => executeBefore.Add(con, Array.Empty<Expression>());

        private void Visit(Expression expr) => Visit((dynamic)expr);
    }
}
