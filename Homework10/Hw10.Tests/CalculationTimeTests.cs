using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Homework10.Tests;

public class CalculationTimeTests: IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public CalculationTimeTests(TestApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
    }
    
    [Theory]
    [InlineData("1 + 1 + 1 + 1")]
    [InlineData("2 * (3 + 2) / 2")]
    [InlineData("2 * 3 / 1 * 5 * 6")]
    private async Task CorrectResultWithTwoSameRequests(string expression)
    {
        await GetRequestExecutionTime(expression);
        var secondCalculationTime = await GetRequestExecutionTime(expression);
        Assert.True(secondCalculationTime <= 2000);
    }
    
    private async Task<long> GetRequestExecutionTime(string expression)
    {
        var watch = Stopwatch.StartNew();
        var response = await _client.PostCalculateExpressionAsync(expression);
        watch.Stop();
        response.EnsureSuccessStatusCode();
        return watch.ElapsedMilliseconds;
    }
}