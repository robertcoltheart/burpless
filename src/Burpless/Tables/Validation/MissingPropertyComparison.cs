namespace Burpless.Tables.Validation;

internal class MissingPropertyComparison(ComparisonType type, string column) : IComparison
{
    public ComparisonType Type { get; } = type;

    public void Format(DifferenceBuilder builder)
    {
        builder.AppendDifference(Type, column);
    }
}
