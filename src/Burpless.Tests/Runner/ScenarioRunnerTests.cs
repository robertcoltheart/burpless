﻿using Burpless.Runner;
using FakeItEasy;

namespace Burpless.Tests.Runner;

public class ScenarioRunnerTests
{
    private readonly IServiceProvider services = A.Fake<IServiceProvider>();

    private readonly Context context = new();

    private readonly FeatureContext featureContext = new();

    public ScenarioRunnerTests()
    {
        A.CallTo(() => services.GetService(typeof(Context))).Returns(context);
        A.CallTo(() => services.GetService(typeof(FeatureContext))).Returns(featureContext);
    }

    [Test]
    public async Task CanRunGivenStep()
    {
        var details = new ScenarioDetails<Context>();
        details.Steps.Add(new ScenarioStep<Context>("Given", StepType.Given, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(1);
    }

    [Test]
    public async Task CanRunWhenStep()
    {
        var details = new ScenarioDetails<Context>();
        details.Steps.Add(new ScenarioStep<Context>("When", StepType.When, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(1);
    }

    [Test]
    public async Task CanRunThenStep()
    {
        var details = new ScenarioDetails<Context>();
        details.Steps.Add(new ScenarioStep<Context>("Then", StepType.Then, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(1);
    }

    [Test]
    public async Task CanRunFeatureAndStepsWithSameContext()
    {
        var details = new ScenarioDetails<Context>();
        details.Feature = Feature.Named("Feature")
            .WithBackground<Context>(x => x.Given(c => c.Call()));
        details.Steps.Add(new ScenarioStep<Context>("Then", StepType.Then, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(2);
        A.CallTo(() => services.GetService(typeof(Context))).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task CanRunFeatureAndStepsWithDifferentContexts()
    {
        var details = new ScenarioDetails<Context>();
        details.Feature = Feature.Named("Feature")
            .WithBackground<FeatureContext>(x => x.Given(c => c.Call()));
        details.Steps.Add(new ScenarioStep<Context>("Then", StepType.Then, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(1);
        await Assert.That(featureContext.Called).IsEqualTo(1);

        A.CallTo(() => services.GetService(typeof(Context))).MustHaveHappenedOnceExactly();
        A.CallTo(() => services.GetService(typeof(FeatureContext))).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task IgnoresFeatureWithNoBackground()
    {
        var details = new ScenarioDetails<Context>();
        details.Feature = Feature.Named("Feature");
        details.Steps.Add(new ScenarioStep<Context>("Then", StepType.Then, c => c.Call()));

        var runner = new ScenarioRunner<Context>(services, details);
        await runner.Execute();

        await Assert.That(context.Called).IsEqualTo(1);

        A.CallTo(() => services.GetService(typeof(Context))).MustHaveHappenedOnceExactly();
        A.CallTo(() => services.GetService(typeof(FeatureContext))).MustNotHaveHappened();
    }

    private class Context
    {
        public int Called;

        public Task Call()
        {
            Called++;

            return Task.CompletedTask;
        }
    }

    private class FeatureContext : Context;
}
