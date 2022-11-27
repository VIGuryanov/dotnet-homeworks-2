using Microsoft.AspNetCore.Mvc.Testing;

namespace Hw12;

public class TestApplicationFactoryCSharp : WebApplicationFactory<TestApplicationFactoryCSharp>
    // TODO: replace generic argument with the right one
{
    public new HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7008");
        return client;
    }
}