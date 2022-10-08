module Hw5.Parser

open System
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length with
    | 3 -> Ok args
    | _ -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | (CalculatorOperation.Plus | CalculatorOperation.Minus |
        CalculatorOperation.Multiply | CalculatorOperation.Divide) -> Ok (arg1, operation, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseValue (value: string): Result<'a, Message> =
    let flag, _val = System.Decimal.TryParse(value)
    if flag then Ok _val
    else Error Message.WrongArgFormat

let parseOperation(operation: string): Result<CalculatorOperation, Message> =
    match operation with
    | "+" -> Ok CalculatorOperation.Plus
    | "-" -> Ok CalculatorOperation.Minus
    | "*" -> Ok CalculatorOperation.Multiply
    | "/" -> Ok CalculatorOperation.Divide
    | _ -> Error Message.WrongArgFormatOperation


let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    MaybeBuilder.maybe
        {
            let! val1 = args[0] |> parseValue
            let! val2 = args[2] |> parseValue
            let! operation = args[1] |> parseOperation
            return (val1, operation, val2)
        }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation.Divide -> 
        if arg2 = Decimal.Zero then Error Message.DivideByZero
        else Ok (arg1, operation, arg2)
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe
        {
            let! a = args |> isArgLengthSupported 
            let! b = a |> parseArgs
            let! c = b |> isOperationSupported
            let! d = c |> isDividingByZero
            return d
        }