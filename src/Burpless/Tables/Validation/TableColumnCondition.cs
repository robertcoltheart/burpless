namespace Burpless.Tables.Validation;

internal class TableColumnCondition<T>
{
    public required string ColumnName { get; set; }

    public required Func<T, string> ValueParser { get; set; }

    public required Func<T, bool> IsValid { get; set; }
}
