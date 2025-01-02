namespace Burpless;

public class Table
{
    private static readonly TableParser Parser = new();

    public List<string> Columns { get; private set; } = [];

    public List<string?[]> Rows { get; } = [];

    public static implicit operator Table(string value)
    {
        return Parse(value);
    }

    public static Table Parse(string value)
    {
        return Parser.Parse(value);
    }

    public static bool TryParse(string value, out Table table)
    {
        try
        {
            table = Parse(value);

            return true;
        }
        catch
        {
            table = null!;

            return false;
        }
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
        var stringValues = values
            .Select(x => x?.ToString())
            .ToArray();

        Rows.Add(stringValues);

        return this;
    }

    public Table AddRow(params IEnumerable<object?> values)
    {
        var stringValues = values
            .Select(x => x?.ToString())
            .ToArray();

        Rows.Add(stringValues);

        return this;
    }
}
