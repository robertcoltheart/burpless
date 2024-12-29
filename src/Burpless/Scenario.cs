using System.Runtime.CompilerServices;
using Burpless.Builders;

namespace Burpless;

public static class Scenario
{
    public static ScenarioGroupingBuilder<TContext> For<TContext>([CallerMemberName] string? name = null)
        where TContext : class
    {
        return new ScenarioGroupingBuilder<TContext>(name);
    }
}
