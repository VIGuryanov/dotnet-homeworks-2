namespace Hw9.Services.MathCalculator
{
    public enum OperatorType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        OpeningBracket,
        ClosingBracket
    }

    public static class OperatorTypeExtentions
    {
        public static string ToString(OperatorType opType) =>
            opType switch
            {
                OperatorType.Addition => "+",
                OperatorType.Subtraction => "-",
                OperatorType.Multiplication => "*",
                OperatorType.Division => "/",
                OperatorType.OpeningBracket => "(",
                OperatorType.ClosingBracket => ")",
                _ => throw new InvalidOperationException("Unexpected OperationType"),
            };
    }
}
