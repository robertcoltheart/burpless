namespace Burpless.Tables.Validation;

internal class MatchedRowComparison(ComparisonType type, string?[] row) : IComparison
{
    public ComparisonType Type { get; } = type;

    public string?[] Row { get; } = row;

    public void Format(DifferenceBuilder builder)
    {
        var values = Row
            .Select(x => x ?? string.Empty)
            .ToArray();

        builder.AppendDifference(Type, values);
    }
}
