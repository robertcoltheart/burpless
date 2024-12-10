namespace Burpless;

public static class TableExtensions
{
    public static T Get<T>(this Table table)
    {
        return default(T);
    }

    public static IEnumerable<T> GetAll<T>(this Table table)
    {
        return null;
    }
}
