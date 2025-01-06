namespace Burpless.Tables;

internal class TableColumnCondition<T>
{
    public required string ColumnName { get; set; }

    public required Func<T, bool> IsValid { get; set; }

    public required Action<T> Validate { get; set; }
}
