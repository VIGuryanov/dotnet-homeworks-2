using BenchmarkDotNet.Running;
using Hw12;
using StackExchange.Profiling;

var profiler = MiniProfiler.StartNew("New profile");
using (profiler.Step("Benchmark"))
{
    BenchmarkRunner.Run<WebApplicationWorkingTimeTests>();
}
Console.WriteLine(profiler.RenderPlainText());