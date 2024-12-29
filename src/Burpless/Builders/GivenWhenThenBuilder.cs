using System.Linq.Expressions;

namespace Burpless.Builders;

public class GivenWhenThenBuilder<TContext> : ScenarioExecutor<TContext>
    where TContext : class
{
    internal GivenWhenThenBuilder()
    {
    }

    public ContinuationBuilder<TContext> Given(Expression<Action<TContext>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> Given(string step, Action<TContext> action)
    {
        return Given(step, action.ToAsync());
    }

    public ContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Given, action);

        return ContinueWith();
    }

    public ContinuationBuilder<TContext> When(Expression<Action<TContext>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> When(Expression<Func<TContext, Task>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> When(string step, Action<TContext> action)
    {
        return When(step, action.ToAsync());
    }

    public ContinuationBuilder<TContext> When(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.When, action);

        return ContinueWith();
    }

    public ContinuationBuilder<TContext> Then(Expression<Action<TContext>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> Then(Expression<Func<TContext, Task>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ContinuationBuilder<TContext> Then(string step, Action<TContext> action)
    {
        return Then(step, action.ToAsync());
    }

    public ContinuationBuilder<TContext> Then(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Then, action);

        return ContinueWith();
    }

    private ContinuationBuilder<TContext> ContinueWith()
    {
        return new ContinuationBuilder<TContext>
        {
            Details = Details
        };
    }
}
