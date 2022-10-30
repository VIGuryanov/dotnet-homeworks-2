using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public IActionResult Calculate(
        [FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        try
        {
            CalculatorParser.ParseCalcArguments(
                new ReadOnlyCollection<string>(new List<string>() { val1, operation, val2 }),
                out double value1,
                out Operation op,
                out double value2);

            return Ok(calculator.Calculate(value1, op, value2));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}