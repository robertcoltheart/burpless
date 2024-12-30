using System.Linq.Expressions;

namespace Burpless.Builders;

public class BackgroundBuilder<TContext>
    where TContext: class
{
    internal BackgroundBuilder()
    {
    }

    internal List<IScenarioStep> Steps { get; set; } = [];

    public BackgroundContinuationBuilder<TContext> Given(Expression<Action<TContext>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public BackgroundContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public BackgroundContinuationBuilder<TContext> Given(string step, Action<TContext> action)
    {
        return Given(step, action.ToAsync());
    }

    public BackgroundContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action)
    {
        var backgroundStep = new ScenarioStep<TContext>(step, StepType.Given, action);

        Steps.Add(backgroundStep);

        return new BackgroundContinuationBuilder<TContext>
        {
            Steps = Steps
        };
    }
}
