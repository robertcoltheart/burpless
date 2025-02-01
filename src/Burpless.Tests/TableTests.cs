namespace Burpless.Tests;

public class TableTests
{
    [Test]
    [Arguments("""
               Missing opening pipe | Column 2 |
               value 1              | value 2  |
               """)]
    [Arguments("""
               | Too few cells | Column 2 |
               | value 1       |
               """)]
    [Arguments("""
               | Too few cells missing last pipe | Column 2 |
               | value 1
               """)]
    [Arguments("""
               | Missing last pipe | Column 2
               | value 1           | value 2
               """)]
    [Arguments("""
               ab | Random leading chars | Column 2 |
                  | value 1              | value 2 |
               """)]
    public async Task InvalidTablesAreNotParsed(string definition)
    {
        var parsed = Table.TryParse(definition, out _);

        await Assert.That(parsed).IsFalse();
    }

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
        await Assert.That(table.Rows[0].Cast<string>()).Contains("value 1")
            .And.Contains("value 2");
    }

    [Test]
    public async Task CanParseTableWithEscapedPipe()
    {
        const string definition =
            """
            | Column1 | Column \| 2 |
            | value 1 | value 2\|1  |
            """;

        var table = Table.Parse(definition);

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column | 2");
        await Assert.That(table.Rows[0].Cast<string>()).Contains("value 1")
            .And.Contains("value 2|1");
    }

    [Test]
    public async Task CanParseTableWithNewLine()
    {
        const string definition =
            """
            | Column1 | Column \n 2 |
            | value 1 | value 2\n1  |
            """;

        var table = Table.Parse(definition);

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column \n 2");
        await Assert.That(table.Rows[0].Cast<string>()).Contains("value 1")
            .And.Contains("value 2\n1");
    }

    [Test]
    public async Task CanParseTableWithNumbers()
    {
        const string definition =
            """
            | Column1 | Column 2 |
            | 1234    | 123.456  |
            """;

        var table = Table.Parse(definition);

        await Assert.That(table.Rows[0].Cast<string>()).Contains("1234")
            .And.Contains("123.456");
    }

    [Test]
    public async Task CanParseTableImplicitly()
    {
        Table table = """
                      | Column1 | Column 2 |
                      | value 1 | value 2  |
                      """;

        await Assert.That(table).IsNotNull();
    }

    [Test]
    public async Task CanCreateTableFromClass()
    {
        var table = Table.From(new Record("value 1", "value 2"));

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column2");
        await Assert.That(table.Rows).HasCount().EqualTo(1);
        await Assert.That(table.Rows[0].Cast<string>()).Contains("value 1")
            .And.Contains("value 2");
    }

    [Test]
    public async Task CanBuildTableFluently()
    {
        var table = Table
            .WithColumns("Column1", "Column2")
            .AddRow("value 1", "value 2");

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column2");
        await Assert.That(table.Rows).HasCount().EqualTo(1);
        await Assert.That(table.Rows[0].Cast<string>()).Contains("value 1")
            .And.Contains("value 2");
    }

    [Test]
    public async Task CanBuildTableFluentlyWithValues()
    {
        var table = Table
            .WithColumns("Column1", "Column2")
            .AddRow(123, 123.456);

        await Assert.That(table.Columns).Contains("Column1")
            .And.Contains("Column2");
        await Assert.That(table.Rows).HasCount().EqualTo(1);
        await Assert.That(table.Rows[0].Cast<string>()).Contains("123")
            .And.Contains("123.456");
    }

    [Test]
    public async Task CanEnumerateTable()
    {
        var table = Table
            .WithColumns("Column1", "Column2")
            .AddRow(123, 123.456);

        var items = table.ToArray();

        await Assert.That(items).HasCount().EqualTo(2);
        await Assert.That(items[0]).Contains($"Column1").And.Contains($"Column2");
        await Assert.That(items[1]).Contains($"123").And.Contains($"123.456");
    }

    private record Record(string Column1, string Column2);
}
