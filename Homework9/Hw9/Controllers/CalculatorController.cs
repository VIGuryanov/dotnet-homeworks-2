using System.Diagnostics.CodeAnalysis;
using Hw9.Dto;
using Hw9.Services.MathCalculator;
using Hw9.Services.MathCalculator.ExpressionTools;
using Hw9.Services.MathCalculator.Parser;
using Microsoft.AspNetCore.Mvc;

namespace Hw9.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IMathCalculatorService _mathCalculatorService;

        public CalculatorController(IMathCalculatorService mathCalculatorService)
        {
            _mathCalculatorService = mathCalculatorService;
        }
        
        [HttpGet]
        [ExcludeFromCodeCoverage]
        public IActionResult Calculator()
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
}