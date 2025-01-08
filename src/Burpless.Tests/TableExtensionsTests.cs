namespace Burpless.Tests;

public class TableExtensionsTests
{
    [Test]
    public async Task CanDeserializeTable()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            | value         | 123        | 2025-11-24      | 2025-11-25     | 23:11:00         | 123.456        |
            """);

        var row = table.Get<PropertyClass>();

        await Assert.That(row.StringColumn).IsEqualTo("value");
        await Assert.That(row.IntColumn).IsEqualTo(123);
        await Assert.That(row.DateTimeColumn).IsEqualTo(new DateTime(2025, 11, 24));
        await Assert.That(row.DateOnlyColumn).IsEqualTo(new DateOnly(2025, 11, 25));
        await Assert.That(row.TimeOnlyColumn).IsEqualTo(new TimeOnly(23, 11, 0));
        await Assert.That(row.DecimalColumn).IsEqualTo(123.456m);
    }

    [Test]
    public async Task CanDeserializeTableWithNullables()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            | value         | 123        | 2025-11-24      | 2025-11-25     | 23:11:00         | 123.456        |
            """);

        var row = table.Get<NullablePropertyClass>();

        await Assert.That(row.StringColumn).IsEqualTo("value");
        await Assert.That(row.IntColumn).IsEqualTo(123);
        await Assert.That(row.DateTimeColumn).IsEqualTo(new DateTime(2025, 11, 24));
        await Assert.That(row.DateOnlyColumn).IsEqualTo(new DateOnly(2025, 11, 25));
        await Assert.That(row.TimeOnlyColumn).IsEqualTo(new TimeOnly(23, 11, 0));
        await Assert.That(row.DecimalColumn).IsEqualTo(123.456m);
    }

    [Test]
    public async Task CanDeserializeMultipleRows()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            | value         | 123        | 2025-11-24      | 2025-11-25     | 23:11:00         | 123.456        |
            | value         | 123        | 2025-11-24      | 2025-11-25     | 23:11:00         | 123.456        |
            """);

        var row = table.GetAll<PropertyClass>();

        await Assert.That(row).HasCount().EqualTo(2);
    }

    [Test]
    public void NoRowsWillThrowOnDeserialize()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            """);

        Assert.Throws(() => table.Get<PropertyClass>());
    }

    [Test]
    public async Task CanParseEmptyValuesWithNullableTypes()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            |               |            |                 |                |                  |                |
            """);

        var row = table.Get<NullablePropertyClass>();

        await Assert.That(row.StringColumn).IsNullOrEmpty();
        await Assert.That(row.IntColumn).IsNull();
        await Assert.That(row.DateTimeColumn).IsNull();
        await Assert.That(row.DateOnlyColumn).IsNull();
        await Assert.That(row.TimeOnlyColumn).IsNull();
        await Assert.That(row.DecimalColumn).IsNull();
    }

    [Test]
    [Arguments(true, "value")]
    [Arguments(false, "wrong value")]
    public async Task CanCompareTableWithValidator(bool equal, string matchValue)
    {
        var table = Table.Validate<PropertyClass>(validator => validator
            .WithColumn(x => x.StringColumn, x => x == matchValue));

        var data = new PropertyClass
        {
            StringColumn = "value"
        };

        var result = table.AreEqual(data);

        await Assert.That(result).IsEqualTo(equal);
    }

    private class PropertyClass
    {
        public string StringColumn { get; set; } = null!;

        public int IntColumn { get; set; }

        public DateTime DateTimeColumn { get; set; }

        public DateOnly DateOnlyColumn { get; set; }

        public TimeOnly TimeOnlyColumn { get; set; }

        public decimal DecimalColumn { get; set; }
    }

    private class NullablePropertyClass
    {
        public string? StringColumn { get; set; }

        public int? IntColumn { get; set; }

        public DateTime? DateTimeColumn { get; set; }

        public DateOnly? DateOnlyColumn { get; set; }

        public TimeOnly? TimeOnlyColumn { get; set; }

        public decimal? DecimalColumn { get; set; }
    }
}
