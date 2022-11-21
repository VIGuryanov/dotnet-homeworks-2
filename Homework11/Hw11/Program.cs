using System.Diagnostics.CodeAnalysis;
using Hw11.Configuration;
using Hw11.Exceptions;
using Hw11.Services.MathCalculator;
using Hw11.Services.MathCalculator.ExpressionTools;
using Hw11.Services.MathCalculator.Parser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddMathCalculator();
builder.Services.AddTransient<IExceptionHandler, ExceptionHandler>();
builder.Services.AddTransient<IMathExpressionTokenizerParser, MathExpressionTokenizerParser>();
builder.Services.AddTransient<IExpressionToDictionary, ExpressionToDictionary>();
builder.Services.AddTransient<IShuntingYardAlgorithm, ShuntingYardAlgorithm>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Calculator}/{id?}");

app.Run();

namespace Hw11
{
    [ExcludeFromCodeCoverage]
    public partial class Program { }
}