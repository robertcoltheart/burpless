using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class WhenBuilderTests
{
    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void WhenExpressionsAddStep(string name)
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.Work())
            .When(x => x.WorkAsync())
            .When("MyWork", x => x.Work())
            .When("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void WhenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.Dummy())
            .And(x => x.Work())
            .And(x => x.WorkAsync())
            .And("MyWork", x => x.Work())
            .And("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void WhenButContinuationExpressionsAddStep(string name)
    {
        var builder = new WhenBuilder<Context>()
            .When(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.When, step.Type);
    }

    private class Context
    {
        public void Dummy()
        {
        }

        public void Work()
        {
        }

        public Task WorkAsync()
        {
            return Task.CompletedTask;
        }
    }
}
