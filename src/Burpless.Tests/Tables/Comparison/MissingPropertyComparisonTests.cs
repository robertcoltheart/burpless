using Burpless.Tables.Comparison;

namespace Burpless.Tests.Tables.Comparison;

public class MissingPropertyComparisonTests
{
    [Test]
    public async Task MissingPropertyCanBeRendered()
    {
        var comparison = new MissingPropertyComparison(ComparisonType.Missing, "col1");
        var builder = new ComparisonBuilder();

        comparison.Format(builder);

        var result = builder.ToString();

        await Assert.That(result).IsEqualTo(
            """
            - | col1 |
            """);
    }
}
