using System.Diagnostics.CodeAnalysis;
using Hw10.Dto;
using Hw10.Services;
using Hw10.Services.MathCalculator;
using Hw10.Services.MathCalculator.ExpressionTools;
using Hw10.Services.MathCalculator.Parser;
using Microsoft.AspNetCore.Mvc;

namespace Hw10.Controllers;

public class CalculatorController : Controller
{
    private readonly IMathCalculatorService _mathCalculatorService;

    public CalculatorController(IMathCalculatorService mathCalculatorService)
    {
        _mathCalculatorService = mathCalculatorService;
    }
        
    [HttpGet]
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
        public async Task<ActionResult<CalculationMathExpressionResultDto>> CalculateMathExpression(
            [FromServices] IExpressionToDictionary expressionToDictionary,
            [FromServices] IMathExpressionTokenizerParser mathExpressionTokenizerParser,
            [FromServices] IShuntingYardAlgorithm shuntingYardAlgorithm,
            string expression)
        {
            var result = await _mathCalculatorService.CalculateMathExpressionAsync(expression, expressionToDictionary, mathExpressionTokenizerParser, shuntingYardAlgorithm);
            return Json(result);
        }
}