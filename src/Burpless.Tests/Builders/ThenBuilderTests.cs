using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ThenBuilderTests
{
    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Work())
            .Then(x => x.WorkAsync())
            .Then("MyWork", x => x.Work())
            .Then("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Work())
            .Then((x, r) => x.WorkAsync())
            .Then("MyWork", (x, r) => x.Work())
            .Then("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Dummy())
            .And(x => x.Work())
            .And(x => x.WorkAsync())
            .And("MyWork", x => x.Work())
            .And("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenAndContinuationExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Dummy())
            .And((x, r) => x.Work())
            .And((x, r) => x.WorkAsync())
            .And("MyWork", (x, r) => x.Work())
            .And("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenButContinuationExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData("Work")]
    [InlineData("WorkAsync")]
    [InlineData("MyWork")]
    [InlineData("MyWorkAsync")]
    public void ThenButContinuationExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Dummy())
            .But((x, r) => x.Work())
            .But((x, r) => x.WorkAsync())
            .But("MyWork", (x, r) => x.Work())
            .But("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        Assert.NotNull(step);
        Assert.Equal(StepType.Then, step.Type);
    }

    [Theory]
    [InlineData(typeof(InvalidOperationException))]
    [InlineData(typeof(ArgumentException))]
    public void CanExpectException(Type exception)
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown(exception);

        Assert.Equal(exception, builder.Details.ExpectedException);
    }

    [Fact]
    public void CanExpectDefaultException()
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown();

        Assert.Equal(typeof(Exception), builder.Details.ExpectedException);
    }

    [Fact]
    public void CanExpectTypedException()
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown<InvalidOperationException>();

        Assert.Equal(typeof(InvalidOperationException), builder.Details.ExpectedException);
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
