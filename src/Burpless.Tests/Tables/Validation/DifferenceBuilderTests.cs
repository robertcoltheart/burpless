using Burpless.Tables.Validation;

namespace Burpless.Tests.Tables.Validation;

public class DifferenceBuilderTests
{
    [Test]
    public async Task CanRenderHeaders()
    {
        var result = new DifferenceBuilder()
            .AppendTableHeaders(["col1", "col2"])
            .ToString();

        await Assert.That(result).IsEqualTo("  | col1 | col2 |");
    }

    [Test]
    public async Task CanRenderMatchedRows()
    {
        var result = new DifferenceBuilder()
            .AppendTableHeaders(["col1", "col2"])
            .AppendDifference(ComparisonType.Match, ["val1", "val2"])
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
        var result = new DifferenceBuilder()
            .AppendTableHeaders(["col1", "col2"])
            .AppendDifference(ComparisonType.Match, ["val1", "val2"])
            .AppendDifference(ComparisonType.Missing, ["val3", "val4"])
            .AppendDifference(ComparisonType.Additional, ["val5", "val6"])
            .ToString();

        await Assert.That(result).IsEqualTo(
            """
              | col1 | col2 |
              | val1 | val2 |
            - | val3 | val4 |
            + | val5 | val6 |
            """);
    }
}
