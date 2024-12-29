using System.Linq.Expressions;

namespace Burpless.Builders;

public class ContinuationBuilder<TContext> : GivenWhenThenBuilder<TContext>
    where TContext : class
{
    internal ContinuationBuilder()
    {
    }

    public ContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action)
        };
    }

    public ContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action)
        };
    }

    public ContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action)
        };
    }

    public ContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action)
        };
    }

    public ContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action)
        };
    }

    public ContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action)
        };
    }

    public ContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action)
        };
    }

    public ContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action)
        };
    }

    private StepType GetLastStepType()
    {
        return Details.Steps.Last().Type;
    }
}
