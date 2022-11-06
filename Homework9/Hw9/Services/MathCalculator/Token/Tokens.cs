namespace Hw9.Services.MathCalculator.Token
{
    public class OperandToken : IToken
    {
        public readonly double Value;
        public OperandToken(double value)
        {
            Value = value;
        }
    }

    public class OperatorToken : IToken
    {
        public readonly OperatorType Operator;
        public OperatorToken(OperatorType op)
        {
            Operator = op;
        }
    }
}
