open Hw4.Parser
open Hw4.Calculator


let Main args =
    let calcOps = parseCalcArguments args
    printfn $"{calculate calcOps.arg1 calcOps.operation calcOps.arg2}"
