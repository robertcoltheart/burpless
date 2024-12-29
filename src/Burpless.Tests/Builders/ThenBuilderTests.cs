using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ThenBuilderTests
{
    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Work())
            .Then(x => x.WorkAsync())
            .Then("MyWork", x => x.Work())
            .Then("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Work())
            .Then((x, r) => x.WorkAsync())
            .Then("MyWork", (x, r) => x.Work())
            .Then("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Dummy())
            .And(x => x.Work())
            .And(x => x.WorkAsync())
            .And("MyWork", x => x.Work())
            .And("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenAndContinuationExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Dummy())
            .And((x, r) => x.Work())
            .And((x, r) => x.WorkAsync())
            .And("MyWork", (x, r) => x.Work())
            .And("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenButContinuationExpressionsAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenButContinuationExpressionsWithResultAddStep(string name)
    {
        var builder = new ThenBuilder<Context>()
            .Then((x, r) => x.Dummy())
            .But((x, r) => x.Work())
            .But((x, r) => x.WorkAsync())
            .But("MyWork", (x, r) => x.Work())
            .But("MyWorkAsync", (x, r) => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
    }

    [Test]
    [Arguments(typeof(InvalidOperationException))]
    [Arguments(typeof(ArgumentException))]
    public async Task CanExpectException(Type exception)
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown(exception);

        await Assert.That(builder.Details.ExpectedException).IsEqualTo(exception);
    }

    [Test]
    public async Task CanExpectDefaultException()
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown();

        await Assert.That(builder.Details.ExpectedException).IsEqualTo(typeof(Exception));
    }

    [Test]
    public async Task CanExpectTypedException()
    {
        var builder = new ThenBuilder<Context>()
            .ThenExceptionIsThrown<InvalidOperationException>();

        await Assert.That(builder.Details.ExpectedException).IsEqualTo(typeof(InvalidOperationException));
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
