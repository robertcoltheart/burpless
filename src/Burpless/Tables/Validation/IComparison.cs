namespace Burpless.Tables.Validation;

internal interface IComparison
{
    ComparisonType Type { get; }

    void Format(DifferenceBuilder builder);
}
