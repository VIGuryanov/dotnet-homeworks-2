module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Hw5.CalculatorMain
open Hw5
open Microsoft.AspNetCore.Http

let GetErrorMessage(err: Message*string)=
    let item1,item2 = err
    item1

let GetErrorText(err: Message*string)=
    let item1,item2 = err
    item2

let SeparateMathAndClientErrors (err: Message*string): Result<Message*string,Message*string> =
    match GetErrorMessage err with
    |Message.DivideByZero -> Ok err
    |_ -> Error err

let ErrorResultGetText(err: Result<Message*string,Message*string>) =
    match err with
    |Ok err -> Ok (GetErrorText err);
    |Error err -> Error (GetErrorText err);

let CalcResulConverter (value: Result<decimal, Message*string>) = 
    match value with
    |Ok value -> Ok (value.ToString());
    |Error value -> ErrorResultGetText (SeparateMathAndClientErrors value);

let FormatQueryCalcOperator (op : string): string = 
    match op with
    |"Plus" -> "+"
    |"Minus" -> "-"
    |"Multiply" -> "*"
    |"Divide" -> "/"
    |_ -> op

let GetQueryCalcParams (ctx: HttpContext): string[] =
    let queryColl = ctx.Request.Query
    [|queryColl["value1"].ToString(); FormatQueryCalcOperator (queryColl["operation"].ToString()); queryColl["value2"].ToString()|]

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = CalcResulConverter (Main (GetQueryCalcParams ctx))

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
        ]
        calculatorHandler
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0
