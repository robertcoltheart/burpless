using Burpless.Tables.Comparison;

namespace Burpless.Tests.Tables.Comparison;

public class RowComparisonTests
{
    [Test]
    public async Task CanRenderMissingRowToString()
    {
        var row = new[] { "val1", "val2" };

        var comparison = new RowComparison(ComparisonType.Missing, row);
        var builder = new ComparisonBuilder();

        comparison.Format(builder);

        var result = builder.ToString();

        await Assert.That(result).IsEqualTo(
            """
            - | val1 | val2 |
            """);
    }

    [Test]
    public async Task CanRenderAdditionalRowToString()
    {
        var row = new[] { "val1", "val2" };

        var comparison = new RowComparison(ComparisonType.Additional, row);
        var builder = new ComparisonBuilder();

        comparison.Format(builder);

        var result = builder.ToString();

        await Assert.That(result).IsEqualTo(
            """
            + | val1 | val2 |
            """);
    }
}
