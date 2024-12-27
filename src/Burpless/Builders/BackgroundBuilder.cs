using System.Linq.Expressions;

namespace Burpless.Builders;

public class BackgroundBuilder<TContext>
    where TContext: class
{
    internal BackgroundBuilder()
    {
    }

    internal List<ScenarioStep<TContext>> Steps { get; set; } = [];

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
        var backgroundStep = new ScenarioStep<TContext>(step, StepType.Given, (context, _) => action(context));

        Steps.Add(backgroundStep);

        return new BackgroundContinuationBuilder<TContext>();
    }
}
