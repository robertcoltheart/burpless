using System.Linq.Expressions;

namespace Burpless;

public interface ITableValidator<T>
    where T : new()
{
    ITableValidator<T> WithColumn<TValue>(Expression<Func<T, TValue>> expression, Predicate<TValue> condition);

    ITableValidator<T> WithColumn<TValue>(string columnName, Func<T, TValue> expression, Predicate<TValue> condition);

    ITableValidator<T> WithData(params IEnumerable<T> data);
}
