using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class WhenBuilderTests
{
    [Fact]
    public void WhenExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
        Assert.Equal("Work", step.Name);
    }

    [Fact]
    public void WhenAsyncExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
        Assert.Equal("WorkAsync", step.Name);
    }

    [Fact]
    public void WhenNamedExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When("MyWork", x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
        Assert.Equal("MyWork", step.Name);
    }

    [Fact]
    public void WhenNamedAsyncExpressionsAddStep()
    {
        var builder = new WhenBuilder<Context>()
            .When("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
        Assert.Equal("MyWorkAsync", step.Name);
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
