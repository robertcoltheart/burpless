using System.Collections;
using Burpless.Tables;
using Burpless.Tables.Comparison;
using Burpless.Tables.Validation;

namespace Burpless;

/// <summary>
/// Defines a tabular set of data using columns and rows.
/// </summary>
/// <remarks>
/// The <see cref="Table"/> is compatible with the Gherkin specification for data tables.
/// </remarks>
public class Table : IEnumerable<string?[]>
{
    private static readonly TableParser Parser = new();

    private readonly List<string> columns = [];

    private readonly List<string?[]> rows = [];

    /// <summary>
    /// The columns of the table.
    /// </summary>
    public IReadOnlyList<string> Columns => columns.AsReadOnly();

    /// <summary>
    /// The rows of the table.
    /// </summary>
    public IReadOnlyList<string?[]> Rows => rows.AsReadOnly();

    internal ITableValidatorExecutor? Validator { get; private set; }

    /// <summary>
    /// Defines an implicit conversion of a Gherkin data table to a <see cref="Table"/>.
    /// </summary>
    /// <param name="value">A Gherkin data table definition to be converted to a <see cref="Table"/>.</param>
    /// <returns>A table that is parsed from the string value.</returns>
    /// <exception cref="ArgumentException">Gherkin table is invalid.</exception>
    public static implicit operator Table(string value)
    {
        return Parse(value);
    }

    /// <summary>
    /// Parses a Gherkin data table to a <see cref="Table"/>.
    /// </summary>
    /// <param name="value">A Gherkin data table definition to be converted to a <see cref="Table"/>.</param>
    /// <returns>A table that is parsed from the string value.</returns>
    /// <exception cref="ArgumentException">Gherkin table is invalid.</exception>
    public static Table Parse(string value)
    {
        return Parser.Parse(value);
    }

    /// <summary>
    /// Parses a Gherkin data table to a <see cref="Table"/>. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">A Gherkin data table definition to be converted to a <see cref="Table"/>.</param>
    /// <param name="table">When this method returns, contains the <see cref="Table"/> instance containing the parsed Gherkin table.</param>
    /// <returns><see langword="true" /> if <paramref name="value" /> was converted successfully; otherwise, <see langword="false" />.</returns>
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

    /// <summary>
    /// Creates a table with columns and rows derived from the specified type's properties via reflection.
    /// </summary>
    /// <typeparam name="T">The type of the collection to use for creating the <see cref="Table"/>.</typeparam>
    /// <param name="values">The values that will be converted to rows in the <see cref="Table"/>.</param>
    /// <returns>The <see cref="Table"/> that contains columns and rows from the specified values.</returns>
    public static Table From<T>(params T[] values)
    {
        return From(values.AsEnumerable());
    }

    /// <summary>
    /// Creates a table with columns and rows derived from the specified type's properties via reflection.
    /// </summary>
    /// <typeparam name="T">The type of the collection to use for creating the <see cref="Table"/>.</typeparam>
    /// <param name="values">The values that will be converted to rows in the <see cref="Table"/>.</param>
    /// <returns>The <see cref="Table"/> that contains columns and rows from the specified values.</returns>
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

    /// <summary>
    /// Creates a table with the specified column names.
    /// </summary>
    /// <param name="columns">The column names to use for the table.</param>
    /// <returns>An empty table with the specified columns.</returns>
    public static Table WithColumns(params string[] columns)
    {
        return WithColumns(columns.AsEnumerable());
    }

    /// <summary>
    /// Creates a table with the specified column names.
    /// </summary>
    /// <param name="columns">The column names to use for the table.</param>
    /// <returns>An empty table with the specified columns.</returns>
    public static Table WithColumns(params IEnumerable<string> columns)
    {
        return new Table()
            .AddColumns(columns);
    }

    /// <summary>
    /// Creates a pseudo-table that can be used to validate a set of data.
    /// </summary>
    /// <remarks>
    /// Table validators can be used to fluently verify a set of data using logic. Note that a table validator cannot contain any data rows, and attempts to do so will result in an exception.
    /// </remarks>
    /// <typeparam name="T">The type of model to validate with this table validator.</typeparam>
    /// <param name="configure">A delegate for configuring the table validator.</param>
    /// <returns>A table that can be used to validate a collection.</returns>
    public static Table Validate<T>(Action<ITableValidator<T>> configure)
        where T : new()
    {
        var validator = new TableValidator<T>();
        configure(validator);

        var columns = validator.Conditions
            .Select(x => x.ColumnName);

        var table = WithColumns(columns);

        table.Validator = validator;

        return table;
    }

    /// <summary>
    /// Adds the specified columns to the table
    /// </summary>
    /// <param name="columnNames">The collection of column names to add to the table.</param>
    /// <returns>The table instance.</returns>
    public Table AddColumns(params IEnumerable<string> columnNames)
    {
        VerifyValidator();

        columns.AddRange(columnNames);

        return this;
    }

    /// <summary>
    /// Adds a row of specified values to the table.
    /// </summary>
    /// <param name="values">The collection of row values to add to a new row in the table.</param>
    /// <returns>The table instance.</returns>
    public Table AddRow(params string?[] values)
    {
        return AddRow(values.AsEnumerable());
    }

    /// <summary>
    /// Adds a row of specified values to the table.
    /// </summary>
    /// <param name="values">The collection of row values to add to a new row in the table.</param>
    /// <returns>The table instance.</returns>
    public Table AddRow(params IEnumerable<string?> values)
    {
        VerifyValidator();

        rows.Add(values.ToArray());

        return this;
    }

    /// <summary>
    /// Adds a row of specified values to the table, stored as strings.
    /// </summary>
    /// <param name="values">The collection of row values to add to a new row in the table.</param>
    /// <returns>The table instance.</returns>
    public Table AddRow(params object?[] values)
    {
        return AddRow(values.AsEnumerable());
    }

    /// <summary>
    /// Adds a row of specified values to the table, stored as strings.
    /// </summary>
    /// <param name="values">The collection of row values to add to a new row in the table.</param>
    /// <returns>The table instance.</returns>
    public Table AddRow(params IEnumerable<object?> values)
    {
        VerifyValidator();

        var stringValues = values
            .Select(x => x?.ToString())
            .ToArray();

        rows.Add(stringValues);

        return this;
    }

    private void VerifyValidator()
    {
        if (Validator != null)
        {
            throw new InvalidOperationException("Cannot add data when using table validation");
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the columns and then the rows of the <see cref="Table"/>.
    /// </summary>
    /// <returns>An enumerator that iterates through the columns and then the rows of the <see cref="Table"/>.</returns>
    public IEnumerator<string?[]> GetEnumerator()
    {
        var table = new List<string?[]>
        {
            Columns.ToArray()
        };

        table.AddRange(Rows);

        return table.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the columns and then the rows of the <see cref="Table"/>.
    /// </summary>
    /// <returns>An enumerator that iterates through the columns and then the rows of the <see cref="Table"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new ComparisonBuilder()
            .AppendTableHeaders(Columns);

        foreach (var row in Rows)
        {
            builder.AppendRowDifference(ComparisonType.Match, row.Select(x => x ?? string.Empty));
        }

        return builder.ToString();
    }
}
