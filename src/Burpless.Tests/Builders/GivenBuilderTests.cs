using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class GivenBuilderTests
{
    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task GivenExpressionsAddStep(string name)
    {
        var builder = new GivenBuilder<Context>()
            .Given(x => x.Work())
            .Given(x => x.WorkAsync())
            .Given("MyWork", x => x.Work())
            .Given("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Given);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task GivenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new GivenBuilder<Context>()
            .Given(x => x.Dummy())
            .And(x => x.Work())
            .And(x => x.WorkAsync())
            .And("MyWork", x => x.Work())
            .And("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Given);
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task GivenButContinuationExpressionsAddStep(string name)
    {
        var builder = new GivenBuilder<Context>()
            .Given(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Given);
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
