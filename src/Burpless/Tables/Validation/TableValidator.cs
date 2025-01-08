using System.Linq.Expressions;

namespace Burpless.Tables.Validation;

internal class TableValidator<T> : ITableValidator<T>, ITableValidatorExecutor
    where T : new()
{
    public List<TableColumnCondition<T>> Conditions { get; } = new();

    public ITableValidator<T> WithColumn<TValue>(Expression<Func<T, TValue>> expression, Predicate<TValue> condition)
    {
        var columnName = expression.GetName();

        return WithColumn(columnName, expression.Compile(), condition);
    }

    public ITableValidator<T> WithColumn<TValue>(string columnName, Func<T, TValue> expression, Predicate<TValue> condition)
    {
        var column = new TableColumnCondition<T>
        {
            ColumnName = columnName,
            ValueParser = row => expression(row)?.ToString() ?? string.Empty,
            IsValid = row => condition(expression(row))
        };

        Conditions.Add(column);

        return this;
    }

    public bool IsValid(string column, object item, out string value)
    {
        if (item is not T typedItem)
        {
            throw new TableValidationException($"Mismatched table validation type, expected {typeof(T)}, got {item.GetType()}");
        }

        var conditions = Conditions
            .Where(x => x.ColumnName == column.GetColumnName())
            .ToArray();

        value = conditions
            .Select(x => x.ValueParser(typedItem))
            .LastOrDefault() ?? string.Empty;

        return conditions.All(x => x.IsValid(typedItem));
    }
}
