namespace Burpless;

internal static class ActionExtensions
{
    public static Func<T, Task> ToAsync<T>(this Action<T> action)
    {
        return param =>
        {
            action(param);

            return Task.CompletedTask;
        };
    }
}
