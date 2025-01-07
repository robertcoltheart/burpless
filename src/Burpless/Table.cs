using Burpless.Tables;
using Burpless.Tables.Validation;

namespace Burpless;

public class Table
{
    private static readonly TableParser Parser = new();

    public IList<string> Columns { get; private set; } = [];

    public IList<string?[]> Rows { get; } = [];

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
        return From(values.AsEnumerable());
    }

    public static Table From<T>(params IEnumerable<T> values)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.CanRead)
            .ToArray();

        var columns = properties
            .Select(p => p.Name)
            .ToArray();

        var table = WithColumns(columns);

        foreach (var value in values)
        {
            var cells = properties
                .Select(x => x.GetValue(value))
                .Select(x => x?.ToString());

            table.AddRow(cells);
        }

        return table;
    }

    public static Table WithColumns(params string[] columns)
    {
        return WithColumns(columns.AsEnumerable());
    }

    public static Table WithColumns(params IEnumerable<string> columns)
    {
        return new Table
        {
            Columns = [..columns]
        };
    }

    public static Table Validate<T>(Action<ITableValidator<T>> configure)
        where T : new()
    {
        var validator = new TableValidator<T>();
        configure(validator);

        var columns = validator.Conditions
            .Select(x => x.ColumnName);

        return new Table
        {
            Columns = [..columns]
        };
    }

    public Table AddRow(params string?[] values)
    {
        return AddRow(values.AsEnumerable());
    }

    public Table AddRow(params IEnumerable<string?> values)
    {
        Rows.Add(values.ToArray());

        return this;
    }

    public Table AddRow(params object?[] values)
    {
        return AddRow(values.AsEnumerable());
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
