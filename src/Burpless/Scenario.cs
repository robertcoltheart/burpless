using System.Diagnostics;
using Burpless.Builders;
using Burpless.Runner;

namespace Burpless;

public static class Scenario
{
    private static readonly ScenarioRunnerFactory RunnerFactory = new();

    public static ScenarioGroupingBuilder<TContext> For<TContext>()
        where TContext : class
    {
        var name = GetCallingName();

        throw new NotImplementedException();
    }

    private static string GetCallingName()
    {
        var stack = new StackTrace();

        var frame = stack.GetFrame(2);

        return frame?.GetMethod().Name ?? "Scenario";
    }
}
