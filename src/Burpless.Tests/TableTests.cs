namespace Burpless.Tests;

public class TableTests
{
    [Test]
    public async Task CanParseSimpleTable()
    {
        const string definition =
            """
            | Column1 | Column 2 |
            | value 1 | value 2  |
            """;

        var table = Table.Parse(definition);

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column 2");
        await Assert.That(table.Rows).HasCount().EqualTo(1);
        await Assert.That(table.Rows.First()).Contains("value 1")
            .And.Contains("value 2");
    }
}
