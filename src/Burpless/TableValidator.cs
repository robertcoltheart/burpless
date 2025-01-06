using System.Linq.Expressions;
using Burpless.Tables;

namespace Burpless;

public class TableValidator<T> : ITableValidator<T>
    where T : new()
{
    private readonly TableColumnFormatter formatter = new();

    private readonly List<TableColumnCondition<T>> conditions = new();

    private readonly List<T> comparisonData = new();

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
            IsValid = row => condition(expression(row)),
            Validate = row => Validate(row, columnName)
        };

        conditions.Add(column);

        return this;
    }

    public ITableValidator<T> WithData(params IEnumerable<T> data)
    {
        comparisonData.AddRange(data);

        return this;
    }

    public bool IsValid(Table table)
    {
        var rows = table.GetAll<T>();

        return rows.All(row => conditions.All(condition => condition.IsValid(row)));
    }

    public void Validate(Table table)
    {

    }

    private void Validate(T row, string columnName)
    {

    }
}
