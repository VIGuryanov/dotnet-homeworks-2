using Microsoft.AspNetCore.Mvc.Testing;

namespace Hw12;

public class TestApplicationFactoryFSharp : WebApplicationFactory<TestApplicationFactoryFSharp>
    // TODO: replace generic argument with the right one
{
    public new HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:59943");
        return client;
    }
}