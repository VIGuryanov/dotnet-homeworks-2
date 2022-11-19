using Hw11.Dto;
using Hw11.ErrorMessages;
using Hw11.Services.MathCalculator.ExpressionTools;
using Hw11.Services.MathCalculator.Parser;
using Hw11.Exceptions;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(
        string? expression,
        IExpressionToDictionary expressionToDictionary,
        IMathExpressionTokenizerParser mathExpressionTokenizerParser,
        IShuntingYardAlgorithm shuntingYardAlgorithm)
    {
        if (expression == null || expression.Length == 0)
            throw new InvalidSyntaxException(MathErrorMessager.EmptyString);

        var tokenized = mathExpressionTokenizerParser.Parse(expression);

        var postfixExpression = shuntingYardAlgorithm.Apply(tokenized);

        var expressionTree = ExpressionPostfixModelParser.Parse(postfixExpression);

        var dictionaryExpression = expressionToDictionary.Convert(expressionTree);

        return await DictionaryExpressionExecute.ExecuteAsync(dictionaryExpression);
    }
}