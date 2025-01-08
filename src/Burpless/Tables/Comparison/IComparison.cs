namespace Burpless.Tables.Comparison;

internal interface IComparison
{
    ComparisonType Type { get; }

    ElementType Element { get; }

    void Format(ComparisonBuilder builder);
}
