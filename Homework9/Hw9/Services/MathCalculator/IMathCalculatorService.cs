using Hw9.Dto;
using Hw9.Services.MathCalculator.ExpressionTools;
using Hw9.Services.MathCalculator.Parser;

namespace Hw9.Services.MathCalculator;

public interface IMathCalculatorService
{
    public Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(
        string? expression,
        IExpressionToDictionary expressionToDictionary,
        IMathExpressionTokenizerParser mathExpressionTokenizerParser,
        IShuntingYardAlgorithm shuntingYardAlgorithm);
}