using Hw11.Dto;
using Hw11.Services.MathCalculator.ExpressionTools;
using Hw11.Services.MathCalculator.Parser;

namespace Hw11.Services.MathCalculator;

public interface IMathCalculatorService
{
    public Task<double> CalculateMathExpressionAsync(
        string? expression,
        IExpressionToDictionary expressionToDictionary,
        IMathExpressionTokenizerParser mathExpressionTokenizerParser,
        IShuntingYardAlgorithm shuntingYardAlgorithm);
}