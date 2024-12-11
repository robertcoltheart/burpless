namespace Burpless;

public class Table
{
    public List<string> Columns { get; private set; } = [];

    public List<object?[]> Rows { get; } = [];

    public static implicit operator Table(string value)
    {
        return null;
    }

    public static Table From<T>(params T[] values)
    {
        return new Table();
    }

    public static Table From<T>(params IEnumerable<T> values)
    {
        return new Table();
    }

    public static Table WithColumns(params string[] columns)
    {
        return new Table
        {
            Columns = [.. columns]
        };
    }

    public static Table WithColumns(params IEnumerable<string> columns)
    {
        return new Table
        {
            Columns = [..columns]
        };
    }

    public Table AddRow(params object?[] values)
    {
        Rows.Add(values.ToArray());

        return this;
    }

    public Table AddRow(params IEnumerable<object?> values)
    {
        Rows.Add(values.ToArray());

        return this;
    }
}
