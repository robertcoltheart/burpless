namespace Burpless;

public static class TableAssert
{
    public static void Equal<T>(Table expected, params IEnumerable<T> actual)
    {
    }

    public static void Equal<T>(Table expected, params T[] actual)
    {
    }

    public static void Equivalent<T>(Table expected, params IEnumerable<T> actual)
    {
    }

    public static void Equivalent<T>(Table expected, params T[] actual)
    {
    }
}
