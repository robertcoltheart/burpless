using Burpless.Tables.Comparison;

namespace Burpless.Tests.Tables.Comparison;

public class TableDataComparerTests
{
    [Test]
    public async Task CanMatchRows()
    {
        var items = new[]
        {
            new Model
            {
                Id = 1,
                String = "value",
                DateTime = new DateTime(2020, 12, 10),
            },
            new Model
            {
                Id = 2,
                String = "value2",
                DateTime = new DateTime(2020, 12, 11),
            }
        };

        var table = Table.From(items);

        var comparer = new TableDataComparer<Model>();
        var results = comparer.Compare(table, items).ToArray();

        await Assert.That(results).HasCount().EqualTo(2);
        await Assert.That(results[0]).HasMember(x => x.Type).EqualTo(ComparisonType.Match);
        await Assert.That(results[1]).HasMember(x => x.Type).EqualTo(ComparisonType.Match);
    }

    [Test]
    public async Task MismatchedRowsAreRecorded()
    {
        var items = new[]
        {
            new Model
            {
                Id = 1,
                String = "value",
                DateTime = new DateTime(2020, 12, 10),
            },
            new Model
            {
                Id = 2,
                String = "value2",
                DateTime = new DateTime(2020, 12, 11),
            }
        };

        var table = Table
            .WithColumns("Id", "String", "DateTime")
            .AddRow("1", "value", "2020-12-10")
            .AddRow("3", "value3", "2020-12-10");

        var comparer = new TableDataComparer<Model>();
        var results = comparer.Compare(table, items).ToArray();

        var match = results.FirstOrDefault(x => x.Type == ComparisonType.Match);
        var missing = results.FirstOrDefault(x => x.Type == ComparisonType.Missing);
        var additional = results.FirstOrDefault(x => x.Type == ComparisonType.Additional);

        await Assert.That(match).IsNotNull();
        await Assert.That(missing).IsNotNull();
        await Assert.That(additional).IsNotNull();
    }

    [Test]
    [Arguments(true, new[] { "1", "string-value", "true" })]
    [Arguments(false, new[] { "2", "string-value", "true" })]
    [Arguments(false, new[] { "1", "wrong-value", "true" })]
    [Arguments(false, new[] { "1", "string-value", "false" })]
    public async Task ValuesCanBeComparedWithRow(bool equal, string[] values)
    {
        var comparer = new TableDataComparer<ClassWithProperties>();

        var table = Table
            .WithColumns("IntValue", "StringValue", "BoolValue")
            .AddRow(values);

        var item = new ClassWithProperties
        {
            IntValue = 1,
            StringValue = "string-value",
            BoolValue = true,
        };

        var result = comparer.Compare(table, [item]);

        var comparisonType = equal
            ? ComparisonType.Match
            : ComparisonType.Missing;

        var value = result.SingleOrDefault(x => x.Type == comparisonType);

        await Assert.That(value).IsNotNull();
    }

    [Test]
    public async Task MissingColumnReturnsEmptyValue()
    {
        var table = Table
            .WithColumns("missing", "StringValue")
            .AddRow("1", "value");

        var model = new ClassWithProperties
        {
            IntValue = 1,
            StringValue = "wrong",
        };

        var comparer = new TableDataComparer<ClassWithProperties>();
        var builder = new ComparisonBuilder();

        var results = comparer.Compare(table, [model]);

        var match = results.FirstOrDefault(x => x.Type == ComparisonType.Missing);
        match?.Format(builder);

        var value = builder.ToString();

        await Assert.That(match).IsNotNull();
        await Assert.That(value).IsEqualTo("- | 1 | value |");
    }

    [Test]
    public async Task EmptyStringIsTreatedAsNull()
    {
        var table = Table
            .WithColumns("IntValue", "StringValue", "BoolValue")
            .AddRow("1", "", true);

        var model = new ClassWithProperties
        {
            IntValue = 1,
            StringValue = null!,
            BoolValue = true
        };

        var comparer = new TableDataComparer<ClassWithProperties>();
        var builder = new ComparisonBuilder();

        var results = comparer.Compare(table, [model]).ToArray();

        await Assert.That(results).HasCount(1);
        await Assert.That(results[0].Type).IsEqualTo(ComparisonType.Match);
    }

    private class ClassWithProperties
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; } = null!;

        public bool BoolValue { get; set; }
    }

    private class Model
    {
        public int Id { get; set; }

        public string String { get; set; } = null!;

        public DateTime? DateTime { get; set; }
    }
}
