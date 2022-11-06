using System.Diagnostics.CodeAnalysis;
using Hw9.Configuration;
using Hw9.Services.MathCalculator;
using Hw9.Services.MathCalculator.ExpressionTools;
using Hw9.Services.MathCalculator.Parser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMathExpressionTokenizerParser, MathExpressionTokenizerParser>();
builder.Services.AddTransient<IExpressionToDictionary, ExpressionToDictionary>();
builder.Services.AddTransient<IShuntingYardAlgorithm, ShuntingYardAlgorithm>();

builder.Services.AddMathCalculator();

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

namespace Hw9
{
    [ExcludeFromCodeCoverage]
    public partial class Program { }
}