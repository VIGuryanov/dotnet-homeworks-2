using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;
using Hw10.Services.MathCalculator.ExpressionTools;
using Hw10.Services.MathCalculator.Parser;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}

    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression, IExpressionToDictionary expressionToDictionary, IMathExpressionTokenizerParser mathExpressionTokenizerParser, IShuntingYardAlgorithm shuntingYardAlgorithm)
    {
        var dbres = _dbContext.SolvingExpressions.Where(solve => solve.Expression == expression);

        if (dbres.Any())
        {
            Thread.Sleep(1000);
            return new CalculationMathExpressionResultDto(dbres.First().Result);
        }

        var result = await _simpleCalculator.CalculateMathExpressionAsync(expression, expressionToDictionary, mathExpressionTokenizerParser, shuntingYardAlgorithm);

        if(result.IsSuccess)
        {
            await _dbContext.AddAsync(new SolvingExpression{Expression = expression, Result = result.Result});
            await _dbContext.SaveChangesAsync();
        }

        return result;
    }
}