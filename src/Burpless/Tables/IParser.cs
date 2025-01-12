namespace Burpless.Tables;

/// <summary>
/// Defines a way to parse string values to a typed value.
/// </summary>
/// <typeparam name="T">The type that is parsed to from a string value.</typeparam>
public interface IParser<out T>
{
    /// <summary>
    /// Parses a string into a value.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="value"/>.</param>
    /// <returns>The result of parsing <paramref name="value"/>.</returns>
    static abstract T Parse(string value, IFormatProvider? provider);
}
