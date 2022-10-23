module Hw5.CalculatorMain

open Hw5.Calculator
open Hw5.Parser
open Hw5.MaybeBuilder

let Main args =
    maybe{
            let! calcOps = args |> parseCalcArguments
            let (val1, operation, val2) = calcOps
            let calcRes = calculate val1 operation val2
            return calcRes
        }