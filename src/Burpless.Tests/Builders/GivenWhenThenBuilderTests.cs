using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class GivenWhenThenBuilderTests
{
    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task GivenExpressionsAddStep(string name)
    {
        var builder = new GivenWhenThenBuilder<Context>()
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
        var builder = new GivenWhenThenBuilder<Context>()
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
        var builder = new GivenWhenThenBuilder<Context>()
            .Given(x => x.Dummy())
            .But(x => x.Work())
            .But(x => x.WorkAsync())
            .But("MyWork", x => x.Work())
            .But("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Given);
    }

    [Test]
    public async Task WhenExpressionsAddStep()
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .When(x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("Work");
    }

    [Test]
    public async Task WhenAsyncExpressionsAddStep()
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .When(x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("WorkAsync");
    }

    [Test]
    public async Task WhenNamedExpressionsAddStep()
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .When("MyWork", x => x.Work());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("MyWork");
    }

    [Test]
    public async Task WhenNamedAsyncExpressionsAddStep()
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .When("MyWorkAsync", x => x.WorkAsync());

        var step = builder.Details.Steps.FirstOrDefault();

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.Name).IsEqualTo("MyWorkAsync");
    }

    [Test]
    [Arguments("Work")]
    [Arguments("WorkAsync")]
    [Arguments("MyWork")]
    [Arguments("MyWorkAsync")]
    public async Task ThenExpressionsAddStep(string name)
    {
        var builder = new GivenWhenThenBuilder<Context>()
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
    public async Task ThenAndContinuationExpressionsAddStep(string name)
    {
        var builder = new GivenWhenThenBuilder<Context>()
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
    public async Task ThenButContinuationExpressionsAddStep(string name)
    {
        var builder = new GivenWhenThenBuilder<Context>()
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
    [Arguments("WorkAsync", typeof(Context))]
    [Arguments("Something", typeof(AnotherContext))]
    [Arguments("SomethingElse", typeof(AnotherContext))]
    public async Task GiveStepsAddAdditionalContexts(string name, Type type)
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .Given(x => x.WorkAsync())
            .Given<AnotherContext>(x => x.Something())
            .And<AnotherContext>(x => x.SomethingElse());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Given);
        await Assert.That(step?.ContextType).IsEqualTo(type);
    }

    [Test]
    [Arguments("WorkAsync", typeof(Context))]
    [Arguments("Something", typeof(AnotherContext))]
    [Arguments("SomethingElse", typeof(AnotherContext))]
    public async Task WhenStepsAddAdditionalContexts(string name, Type type)
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .When(x => x.WorkAsync())
            .When<AnotherContext>(x => x.Something())
            .And<AnotherContext>(x => x.SomethingElse());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.When);
        await Assert.That(step?.ContextType).IsEqualTo(type);
    }

    [Test]
    [Arguments("WorkAsync", typeof(Context))]
    [Arguments("Something", typeof(AnotherContext))]
    [Arguments("SomethingElse", typeof(AnotherContext))]
    public async Task ThenStepsAddAdditionalContexts(string name, Type type)
    {
        var builder = new GivenWhenThenBuilder<Context>()
            .Then(x => x.WorkAsync())
            .Then<AnotherContext>(x => x.Something())
            .And<AnotherContext>(x => x.SomethingElse());

        var step = builder.Details.Steps.FirstOrDefault(x => x.Name == name);

        await Assert.That(step).IsNotNull();
        await Assert.That(step?.Type).IsEqualTo(StepType.Then);
        await Assert.That(step?.ContextType).IsEqualTo(type);
    }

    public class Context
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

    public class AnotherContext
    {
        public void Something()
        {
        }

        public Task SomethingElse()
        {
            return Task.CompletedTask;
        }
    }
}
