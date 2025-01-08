namespace Burpless.Tables.Comparison;

internal class RowComparison(ComparisonType type, string?[] row) : IComparison
{
    public ComparisonType Type { get; } = type;

    public ElementType Element { get; } = ElementType.Row;

    public string?[] Row { get; } = row;

    public void Format(ComparisonBuilder builder)
    {
        var values = Row
            .Select(x => x ?? string.Empty)
            .ToArray();

        builder.AppendRowDifference(Type, values);
    }
}
