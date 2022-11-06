using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Services.MathCalculator.Parser;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        if (expression == null || expression.Length == 0)
            return new CalculationMathExpressionResultDto(MathErrorMessager.EmptyString);

        try
        {
            var tokenized = new MathExpressionTokenizerParser().Parse(expression);

            var postfixExpression = new ShuntingYardAlgorithm().Apply(tokenized);

            var expressionTree = new ExpressionPostfixModelParser().Parse(postfixExpression);

            return new CalculationMathExpressionResultDto(await new CalculatorExpressionVisitor().VisitTree(expressionTree));
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}