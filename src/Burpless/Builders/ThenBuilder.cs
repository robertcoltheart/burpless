using System.Linq.Expressions;

namespace Burpless.Builders;

public class ThenBuilder<TContext> : ScenarioExecutor<TContext>
    where TContext : class
{
    internal ThenBuilder()
    {
    }

    public ThenContinuationBuilder<TContext> Then(Expression<Action<TContext>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ThenContinuationBuilder<TContext> Then(Expression<Action<TContext, StepResult>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ThenContinuationBuilder<TContext> Then(Expression<Func<TContext, Task>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ThenContinuationBuilder<TContext> Then(Expression<Func<TContext, StepResult, Task>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    public ThenContinuationBuilder<TContext> Then(string step, Action<TContext> action)
    {
        return Then(step, action.ToAsync());
    }

    public ThenContinuationBuilder<TContext> Then(string step, Action<TContext, StepResult> action)
    {
        return Then(step, action.ToAsync());
    }

    public ThenContinuationBuilder<TContext> Then(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Then, (context, _) => action(context));

        return new ThenContinuationBuilder<TContext>
        {
            Details = Details
        };
    }

    public ThenContinuationBuilder<TContext> Then(string step, Func<TContext, StepResult, Task> action)
    {
        AddStep(step, StepType.Then, action);

        return new ThenContinuationBuilder<TContext>
        {
            Details = Details
        };
    }

    public ThenContinuationBuilder<TContext> ThenExceptionIsThrown(Type? exceptionType = null)
    {
        Details.ExpectedException = exceptionType ?? typeof(Exception);

        return new ThenContinuationBuilder<TContext>
        {
            Details = Details
        };
    }

    public ThenContinuationBuilder<TContext> ThenExceptionIsThrown<TException>()
        where TException : Exception
    {
        Details.ExpectedException = typeof(TException);

        return new ThenContinuationBuilder<TContext>
        {
            Details = Details
        };
    }
}
