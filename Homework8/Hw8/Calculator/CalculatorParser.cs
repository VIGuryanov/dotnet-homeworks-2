using System.Collections.ObjectModel;

namespace Hw8.Calculator;

public static class CalculatorParser
{
    public static void ParseCalcArguments(ReadOnlyCollection<string> args,
        out double val1,
        out Operation operation,
        out double val2)
    {
        operation = ParseOperation(args[1]);

        if (!double.TryParse(args[0], out val1) | !double.TryParse(args[2], out val2))
            throw new ArgumentException(Messages.InvalidNumberMessage);
    }

    private static Operation ParseOperation(string operation)
    {
        return operation switch
        {
            "Plus" => Operation.Plus,
            "Minus" => Operation.Minus,
            "Multiply" => Operation.Multiply,
            "Divide" => Operation.Divide,
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage),
        };
    }
}