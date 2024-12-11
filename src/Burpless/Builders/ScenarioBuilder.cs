using System.Linq.Expressions;

namespace Burpless.Builders;

internal class ScenarioBuilder<T> : IScenarioGroupingBuilder<T>
    where T : class
{
    public IDescriptionBuilder<T> Name(string name)
    {
        throw new NotImplementedException();
    }

    public IDescriptionBuilder<T> Description(string name)
    {
        throw new NotImplementedException();
    }

    public IDescriptionBuilder<T> Tags(params IEnumerable<string> tags)
    {
        throw new NotImplementedException();
    }

    public IDescriptionBuilder<T> Tags(params string[] tags)
    {
        throw new NotImplementedException();
    }

    public IDescriptionBuilder<T> Feature(Feature feature)
    {
        throw new NotImplementedException();
    }

    public IGivenContinuationBuilder<T> Given(Expression<Action<T>> action)
    {
        throw new NotImplementedException();
    }

    public IGivenContinuationBuilder<T> Given(Expression<Func<T, Task>> action)
    {
        throw new NotImplementedException();
    }

    public IGivenContinuationBuilder<T> Given(string step, Action<T> action)
    {
        throw new NotImplementedException();
    }

    public IGivenContinuationBuilder<T> Given(string step, Func<T, Task> action)
    {
        throw new NotImplementedException();
    }

    public IWhenContinuationBuilder<T> When(Expression<Action<T>> action)
    {
        throw new NotImplementedException();
    }

    public IWhenContinuationBuilder<T> When(Expression<Func<T, Task>> action)
    {
        throw new NotImplementedException();
    }

    public IWhenContinuationBuilder<T> When(string step, Action<T> action)
    {
        throw new NotImplementedException();
    }

    public IWhenContinuationBuilder<T> When(string step, Func<T, Task> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(Expression<Action<T>> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(Expression<Action<T, StepResult>> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(Expression<Func<T, Task>> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(Expression<Func<T, StepResult, Task>> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(string step, Action<T> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(string step, Action<T, StepResult> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(string step, Func<T, Task> action)
    {
        throw new NotImplementedException();
    }

    public IThenContinuationBuilder<T> Then(string step, Func<T, StepResult, Task> action)
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public Task ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}
