namespace Burpless;

public interface IScenarioExecutor<TContext>
    where TContext : class
{
    void Execute();

    Task ExecuteAsync();
}
