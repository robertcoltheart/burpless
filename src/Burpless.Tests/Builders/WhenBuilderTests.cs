using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class WhenBuilderTests
{
    [Test]
    public async Task WhenExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("Work");
    }

    [Test]
    public async Task WhenAsyncExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("WorkAsync");
    }

    [Test]
    public async Task WhenNamedExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When("MyWork", x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("MyWork");
    }

    [Test]
    public async Task WhenNamedAsyncExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("MyWorkAsync");
    }

    private class Context
    {
        public void Work()
        {
        }

        public Task WorkAsync()
        {
            return Task.CompletedTask;
        }
    }
}
