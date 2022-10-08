module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> ArgumentException("Unsupported operation given") |> raise
    
let parseCalcArguments(args : string[])=
    if not (isArgLengthSupported args) then ArgumentException("Invalid count of input arguments") |> raise
    let val1Flag, val1 = Double.TryParse args[0]
    let val2Flag, val2 = Double.TryParse args[2]
    if not val1Flag || not val2Flag 
        then ArgumentException(
                "Wrong request syntax or unsupported type of values given. Format: {value operation value}. As values can be entered all integer or fractional numbers") |> raise
    let operation = parseOperation args[1]
    {arg1 = val1; arg2 = val2; operation = operation}
    

