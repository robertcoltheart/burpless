using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ScenarioExecutorTests
{
    [Test]
    public async Task DetailsNotNullByDefault()
    {
        var executor = new ScenarioExecutor<object>();

        await Assert.That(executor.Details).IsNotNull();
    }

    [Test]
    public async Task CanCastExecutorToTask()
    {
        var executor = new ScenarioExecutor<object>();

        Task value = executor;

        await Assert.That(executor.Details).IsNotNull();
    }

    [Test]
    public async Task CanAwaitExecutor()
    {
        var executor = new ScenarioExecutor<object>();

        await executor;
    }
}
