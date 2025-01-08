using System.Linq.Expressions;

namespace Burpless;

/// <summary>
/// Provides a set of fluent methods for creating table validations.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITableValidator<T>
    where T : new()
{
    /// <summary>
    /// Verifies that a given column satisfies the specified condition.
    /// </summary>
    /// <typeparam name="TValue">The type of the property that is used for validation.</typeparam>
    /// <param name="expression">The expression returning the property value.</param>
    /// <param name="condition">The condition that needs to be satisfied for validation to succeed.</param>
    /// <returns>The validation builder.</returns>
    ITableValidator<T> WithColumn<TValue>(Expression<Func<T, TValue>> expression, Predicate<TValue> condition);
}
