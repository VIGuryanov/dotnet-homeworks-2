// dotcover disable
using System.Diagnostics.CodeAnalysis;
using Hw10.Configuration;
using Hw10.Services.MathCalculator.ExpressionTools;
using Hw10.Services.MathCalculator.Parser;
using Hw10.Services.MathCalculator;
using Hw10.DbModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMathExpressionTokenizerParser, MathExpressionTokenizerParser>();
builder.Services.AddTransient<IExpressionToDictionary, ExpressionToDictionary>();
builder.Services.AddTransient<IShuntingYardAlgorithm, ShuntingYardAlgorithm>();

builder.Services
    .AddMathCalculator()
    .AddCachedMathCalculator();

builder.Services.AddSingleton<ApplicationContext>();

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
    pattern: "{controller=Calculator}/{action=Index}/{id?}");
app.Run();

namespace Hw10
{
    [ExcludeFromCodeCoverage]
    public partial class Program { }
}