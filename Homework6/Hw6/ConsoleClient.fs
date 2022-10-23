module Hw6.ConsoleClient

open System
open System.Net
open Microsoft.FSharp.Control.WebExtensions
open System.IO

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let GetExceptionMesage(wex: WebException) =
    if not (obj.ReferenceEquals(wex.Response, null))
    then
        let errorResponse : WebResponse = wex.Response
        using (new StreamReader(errorResponse.GetResponseStream()))( fun reader ->
            reader.ReadToEnd())
    else 
        "Bad request"

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let DownloadResponse(uri: Uri) =
    try
        let webClient = new WebClient()
        uri 
        |> webClient.AsyncDownloadString
        |> Async.RunSynchronously
    with
    | :? System.Net.WebException as wex -> GetExceptionMesage wex

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let ConsoleClient = 
    async{
        printfn "Enter <value1> <operation(Plus, Minus, Multiply or Divide)> <value2>"
        //while(true) do
        let input = Console.ReadLine()
        printfn $"Request: {input}"
        let args = input.Split(" ")
        if args.Length >= 3            
        then
            let uri = new System.Uri($"https://localhost:59943//calculate?value1={args[0]}&operation={args[1]}&value2={args[2]}")
            printfn $"Response: {DownloadResponse uri}"
        else
            printfn "Response: Wrong args count"
    }