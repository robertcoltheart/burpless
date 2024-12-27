using System.Linq.Expressions;

namespace Burpless.Builders;

public class GivenBuilder<TContext> : WhenBuilder<TContext>
    where TContext : class
{
    internal GivenBuilder()
    {
    }

    public GivenContinuationBuilder<TContext> Given(Expression<Action<TContext>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public GivenContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public GivenContinuationBuilder<TContext> Given(string step, Action<TContext> action)
    {
        return Given(step, action.ToAsync());
    }

    public GivenContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Given, (context, _) => action(context));

        return new GivenContinuationBuilder<TContext>(Details);
    }
}
