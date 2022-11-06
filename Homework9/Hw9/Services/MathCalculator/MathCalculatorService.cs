using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Services.MathCalculator.ExpressionTools;
using Hw9.Services.MathCalculator.Parser;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(
        string? expression,
        IExpressionToDictionary expressionToDictionary,
        IMathExpressionTokenizerParser mathExpressionTokenizerParser,
        IShuntingYardAlgorithm shuntingYardAlgorithm)
    {
        if (expression == null || expression.Length == 0)
            return new CalculationMathExpressionResultDto(MathErrorMessager.EmptyString);

        try
        {
            var tokenized = mathExpressionTokenizerParser.Parse(expression);

            var postfixExpression = shuntingYardAlgorithm.Apply(tokenized);

            var expressionTree = ExpressionPostfixModelParser.Parse(postfixExpression);

            var dictionaryExpression = expressionToDictionary.Convert(expressionTree);

            return new CalculationMathExpressionResultDto(await DictionaryExpressionExecute.ExecuteAsync(dictionaryExpression));
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}