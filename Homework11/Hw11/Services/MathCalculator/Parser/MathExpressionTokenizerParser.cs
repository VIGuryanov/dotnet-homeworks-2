using System.Text;
using Hw11.ErrorMessages;
using Hw11.Services.MathCalculator.Token;
using Hw11.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hw11.Services.MathCalculator.Parser
{
    public class MathExpressionTokenizerParser : IMathExpressionTokenizerParser
    {
        private readonly StringBuilder valueBuilder;
        private readonly List<IToken> infixNotation;
        private int unpairedBrackets = 0;

        public MathExpressionTokenizerParser()
        {
            valueBuilder = new StringBuilder();
            infixNotation = new List<IToken>();
        }

        public IEnumerable<IToken> Parse(string expression)
        {
            foreach (char next in expression)
            {
                ParseCharacter(next);
            }

            if (valueBuilder.Length > 0)
            {
                var token = CreateOperandToken(valueBuilder.ToString());
                valueBuilder.Clear();
                infixNotation.Add(token);
            }

            if (unpairedBrackets != 0)
                throw new InvalidNumberException(MathErrorMessager.IncorrectBracketsNumber);

            if (infixNotation[^1] is OperatorToken opToken && opToken.Operator != OperatorType.ClosingBracket)
                throw new InvalidSyntaxException(MathErrorMessager.EndingWithOperation);

            return infixNotation.ToList();
        }

        private void ParseCharacter(char next)
        {
            if (IsSpacingCharacter(next))
                return;
            if (IsOperatorCharacter(next))
            {
                if (valueBuilder.Length > 0)
                {
                    var token = CreateOperandToken(valueBuilder.ToString());
                    valueBuilder.Clear();
                    infixNotation.Add(token);
                }

                var operatorToken = CreateOperatorToken(next);
                var infixNotationTokensCount = infixNotation.Count;

                if (operatorToken.Operator == OperatorType.OpeningBracket)
                    unpairedBrackets++;
                else if (operatorToken.Operator == OperatorType.ClosingBracket)
                    unpairedBrackets--;

                Validate(infixNotationTokensCount == 0 ? null : infixNotation[^1], operatorToken.Operator);

                //Adds '-' to next possible parsed double value if found "(-"
                if (infixNotationTokensCount > 0 &&
                    infixNotation[^1] is OperatorToken opToken
                    && opToken.Operator == OperatorType.OpeningBracket
                    && operatorToken.Operator == OperatorType.Subtraction)
                {
                    valueBuilder.Append('-');
                    return;
                }

                infixNotation.Add(operatorToken);
                return;
            }
            if (IsValidChar(next))
                valueBuilder.Append(next);
        }

        private static void Validate(IToken? left, OperatorType right)
        {
            if (left == null)
            {
                if (right != OperatorType.OpeningBracket)
                    throw new InvalidSyntaxException(MathErrorMessager.StartingWithOperation);
            }
            if (left is OperatorToken leftOperatorToken)
            {
                if (leftOperatorToken.Operator == OperatorType.OpeningBracket)
                {
                    if (right == OperatorType.Subtraction || right == OperatorType.OpeningBracket || right == OperatorType.ClosingBracket)
                        return;

                    throw new InvalidSymbolException(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(
                        OperatorTypeExtensions.ToString(right)));
                }
                if (leftOperatorToken.Operator != OperatorType.ClosingBracket)
                {
                    if (right == OperatorType.OpeningBracket)
                        return;

                    if (right == OperatorType.ClosingBracket)
                        throw new InvalidSymbolException(MathErrorMessager.OperationBeforeParenthesisMessage(
                            OperatorTypeExtensions.ToString(leftOperatorToken.Operator)));

                    throw new InvalidSymbolException(MathErrorMessager.TwoOperationInRowMessage(
                        OperatorTypeExtensions.ToString(leftOperatorToken.Operator), OperatorTypeExtensions.ToString(right)));
                }
            }
        }

        private static bool IsValidChar(char c)
        {
            if (c >= '0' && c <= '9' || c == '.')
                return true;
            throw new InvalidSymbolException(MathErrorMessager.UnknownCharacterMessage(c));
        }

        private static bool IsOperatorCharacter(char c) =>
            c switch
            {
                '(' or ')' or '+' or '-' or '*' or '/' => true,
                _ => false,
            };


        private static bool IsSpacingCharacter(char c) =>
            c switch
            {
                ' ' => true,
                _ => false,
            };

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static IToken CreateOperandToken(string raw)
        {
            if (double.TryParse(raw, out double result))
                return new OperandToken(result);

            if (raw.Split('.').Length > 1)
                throw new InvalidNumberException(MathErrorMessager.NotNumberMessage(raw));

            throw new InvalidNumberException($"The operand {raw} has an invalid format.");
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static OperatorToken CreateOperatorToken(char c) =>
            c switch
            {
                '(' => new OperatorToken(OperatorType.OpeningBracket),
                ')' => new OperatorToken(OperatorType.ClosingBracket),
                '+' => new OperatorToken(OperatorType.Addition),
                '-' => new OperatorToken(OperatorType.Subtraction),
                '*' => new OperatorToken(OperatorType.Multiplication),
                '/' => new OperatorToken(OperatorType.Division),
                _ => throw new InvalidSymbolException($"There's no a suitable operator for the char {c}"),
            };
    }
}
