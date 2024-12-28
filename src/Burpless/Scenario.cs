using System.Diagnostics;
using Burpless.Builders;

namespace Burpless;

public static class Scenario
{
    public static ScenarioGroupingBuilder<TContext> For<TContext>()
        where TContext : class
    {
        var name = GetCallingName();

        return new ScenarioGroupingBuilder<TContext>(name);
    }

    private static string GetCallingName()
    {
        var stack = new StackTrace();

        var frame = stack.GetFrame(2);

        return frame?.GetMethod().Name ?? "Scenario";
    }
}
