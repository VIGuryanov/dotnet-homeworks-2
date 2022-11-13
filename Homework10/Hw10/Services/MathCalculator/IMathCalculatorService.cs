using Hw10.Dto;
using Hw10.Services.MathCalculator.ExpressionTools;
using Hw10.Services.MathCalculator.Parser;

namespace Hw10.Services.MathCalculator;

public interface IMathCalculatorService
{
    public Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(
        string? expression,
        IExpressionToDictionary expressionToDictionary,
        IMathExpressionTokenizerParser mathExpressionTokenizerParser,
        IShuntingYardAlgorithm shuntingYardAlgorithm);
}