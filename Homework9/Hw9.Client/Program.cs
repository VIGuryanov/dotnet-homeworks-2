public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Enter domain. For example localhost:<number>");
        var domain = Console.ReadLine();

        Console.WriteLine("Enter math expression");
        var client = new HttpClient();

        while (true)
        {
            var expression = Console.ReadLine();

            if (expression == null)
                continue;

            var values = new Dictionary<string, string>
            {
                {"expression", expression }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync($"{domain}/Calculator/CalculateMathExpression", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
        }
    }
}