module Hw5.Parser

open System
open Hw5.Calculator

let wrongParseMessage (value :string) =
    $"Could not parse value '{value}'"

let parseValue (value: string): Result<'a, 'b> =
    let flag, _val = System.Decimal.TryParse(value)
    if flag then Ok _val
    else Error (Message.WrongArgFormat, wrongParseMessage value)

let parseOperation(operation: string): Result<CalculatorOperation, 'm> =
    match operation with
    | Calculator.plus -> Ok CalculatorOperation.Plus
    | Calculator.minus -> Ok CalculatorOperation.Minus
    | Calculator.multiply -> Ok CalculatorOperation.Multiply
    | Calculator.divide -> Ok CalculatorOperation.Divide
    | _ -> Error (Message.WrongArgFormatOperation, wrongParseMessage operation)


let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), 'm> =
    MaybeBuilder.maybe
        {
            let! val1 = args[0] |> parseValue
            let! val2 = args[2] |> parseValue
            let! operation = args[1] |> parseOperation
            return (val1, operation, val2)
        }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), 'm> =
    match operation with
    | CalculatorOperation.Divide -> 
        if arg2 = Decimal.Zero then Error (Message.DivideByZero, "DivideByZero")
        else Ok (arg1, operation, arg2)
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe
        {
            let! c1 = args |> parseArgs
            let! c2 = c1 |> isDividingByZero
            return c2
        }