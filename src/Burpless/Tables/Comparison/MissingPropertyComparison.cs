namespace Burpless.Tables.Comparison;

internal class MissingPropertyComparison(ComparisonType type, string column) : IComparison
{
    public ComparisonType Type { get; } = type;

    public ElementType Element { get; } = ElementType.Column;

    public void Format(ComparisonBuilder builder)
    {
        builder.AppendColumnDifference(Type, column);
    }
}
