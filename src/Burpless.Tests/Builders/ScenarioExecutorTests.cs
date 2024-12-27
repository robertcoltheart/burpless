using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ScenarioExecutorTests
{
    [Fact]
    public void DetailsNotNullByDefault()
    {
        var executor = new ScenarioExecutor<object>();

        Assert.NotNull(executor.Details);
    }

    [Fact]
    public void CanCastExecutorToTask()
    {
        var executor = new ScenarioExecutor<object>();

        Task value = executor;

        Assert.NotNull(value);
    }

    [Fact]
    public async Task CanAwaitExecutor()
    {
        var executor = new ScenarioExecutor<object>();

        await executor;
    }
}
