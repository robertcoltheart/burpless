namespace Burpless;

public class Table
{
    public List<string> Columns { get; } = [];

    public List<object?[]> Rows { get; } = [];

    public static implicit operator Table(string value)
    {
        return null;
    }

    public static Table FromColumns(params string[] columns)
    {
        return new Table();
    }

    public static Table From<T>(params T[] values)
    {
        return new Table();
    }
}
