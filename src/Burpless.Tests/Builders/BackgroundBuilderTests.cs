using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class BackgroundBuilderTests
{
    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void GivenExpressionsAddStep(string name)
    {
        var builder = new BackgroundBuilder<Context>()
            .Given(x => x.Work())
            .Given(x => x.WorkAsync())
            .Given("MyWork", x => x.Work())
            .Given("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Given, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void GivenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new BackgroundBuilder<Context>()
            .Given(x => x.Dummy())
            .And(x => x.Work())
            .And(x => x.WorkAsync())
            .And("MyWork", x => x.Work())
            .And("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Given, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void GivenButContinuationExpressionsAddStep(string name)
    {
        var builder = new BackgroundBuilder<Context>()
            .Given(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Given, step.Type);
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
