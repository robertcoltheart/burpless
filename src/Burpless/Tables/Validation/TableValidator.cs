using System.Linq.Expressions;

namespace Burpless.Tables.Validation;

internal class TableValidator<T> : ITableValidator<T>, ITableValidatorExecutor
    where T : new()
{
    public Type Type { get; } = typeof(T);

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
            IsValid = row => condition(expression(row))
        };

        Conditions.Add(column);

        return this;
    }

    public bool IsValid(params object[] values)
    {
        var items = values.OfType<T>();

        return items.All(row => Conditions.All(condition => condition.IsValid(row)));
    }

    public void Validate(params object[] values)
    {
        var items = values.OfType<T>();
    }

    private IEnumerable<string> GetValidationErrors(IEnumerable<T> items)
    {
        /*
           |   Col1 | Col2 | Col3 |
         + | **a    |      |      |
         + |        |      |      |

         */
        var builder = new DifferenceBuilder();

        foreach (var item in items)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.IsValid(item))
                {

                }
            }
        }
    }
}
