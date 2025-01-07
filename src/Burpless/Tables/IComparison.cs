namespace Burpless.Tables;

internal interface IComparison
{
    ComparisonType Type { get; }

    void Format(DifferenceBuilder builder);
}
