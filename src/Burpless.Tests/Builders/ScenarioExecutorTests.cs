using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ScenarioExecutorTests
{
    [Test]
    public async Task DetailsNotNullByDefault()
    {
        var executor = new MockExecutor();

        await Assert.That(executor.Details).IsNotNull();
    }

    [Test]
    public async Task CanCastExecutorToTask()
    {
        var executor = new MockExecutor();

        Task value = executor;

        await Assert.That(executor.Details).IsNotNull();
    }

    [Test]
    public async Task CanAwaitExecutor()
    {
        var executor = new MockExecutor();

        await executor;
    }

    private class MockExecutor : ScenarioExecutor<object>
    {
    }
}
