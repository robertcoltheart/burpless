using Burpless.Tables.Comparison;

namespace Burpless.Tests.Tables.Comparison;

public class ComparisonBuilderTests
{
    [Test]
    public async Task CanRenderHeaders()
    {
        var result = new ComparisonBuilder()
            .AppendTableHeaders("col1", "col2")
            .ToString();

        await Assert.That(result).IsEqualTo("  | col1 | col2 |");
    }

    [Test]
    public async Task CanRenderMatchedRows()
    {
        var result = new ComparisonBuilder()
            .AppendTableHeaders("col1", "col2")
            .AppendRowDifference(ComparisonType.Match, ["val1", "val2"])
            .ToString();

        await Assert.That(result).IsEqualTo(
            """
              | col1 | col2 |
              | val1 | val2 |
            """);
    }

    [Test]
    public async Task CanRenderUnmatchedRows()
    {
        var result = new ComparisonBuilder()
            .AppendTableHeaders("col1", "col2")
            .AppendRowDifference(ComparisonType.Match, ["val1", "val2"])
            .AppendRowDifference(ComparisonType.Missing, ["val3", "val4"])
            .AppendRowDifference(ComparisonType.Additional, ["val5", "val6"])
            .ToString();

        await Assert.That(result).IsEqualTo(
            """
              | col1 | col2 |
              | val1 | val2 |
            - | val3 | val4 |
            + | val5 | val6 |
            """);
    }

    [Test]
    public async Task CanRenderColumnDifference()
    {
        var result = new ComparisonBuilder()
            .AppendTableHeaders("Missing properties")
            .AppendColumnDifference(ComparisonType.Missing, "Prop1")
            .AppendColumnDifference(ComparisonType.Missing, "Prop2")
            .ToString();

        await Assert.That(result).IsEqualTo(
            """
              | Missing properties |
            - | Prop1              |
            - | Prop2              |
            """);
    }
}
