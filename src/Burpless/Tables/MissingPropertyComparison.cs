namespace Burpless.Tables;

internal class MissingPropertyComparison(ComparisonType type, string column) : IComparison
{
    public ComparisonType Type { get; } = type;

    public void Format(DifferenceBuilder builder)
    {
        builder.AppendDifference(Type, column);
    }
}
