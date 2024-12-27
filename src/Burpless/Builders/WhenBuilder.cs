using System.Linq.Expressions;

namespace Burpless.Builders;

public class WhenBuilder<TContext> : ThenBuilder<TContext>
    where TContext : class
{
    internal WhenBuilder()
    {
    }

    public WhenContinuationBuilder<TContext> When(Expression<Action<TContext>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    public WhenContinuationBuilder<TContext> When(Expression<Func<TContext, Task>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    public WhenContinuationBuilder<TContext> When(string step, Action<TContext> action)
    {
        return When(step, action.ToAsync());
    }

    public WhenContinuationBuilder<TContext> When(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.When, (context, _) => action(context));

        return new WhenContinuationBuilder<TContext>(Details);
    }
}
