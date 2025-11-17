using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
    public async Task NoRowsWillThrowOnDeserialize()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | DateTime Column | DateOnlyColumn | time only column | decimal-column |
            """);

        await Assert.That(() => table.Get<PropertyClass>()).ThrowsException();
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

    [Test]
    public async Task CanToStringTable()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var value = table.ToString();

        await Assert.That(value).IsEqualTo(
            """
              | String Column | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
              | string        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """,
            IgnoreLineEndingStringComparer.Instance);
    }

    [Test]
    public async Task TableAndCollectionAreEqual()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.AreEqual(collection)).IsTrue();
        table.ShouldEqual(collection);
    }

    [Test]
    public async Task TableAndCollectionAreNotEqual()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string1",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.AreEqual(collection)).IsFalse();
        await Assert.That(() => table.ShouldEqual(collection)).Throws<TableValidationException>();
    }

    [Test]
    public async Task TableShouldContainCollection()
    {
        var table = Table.Parse(
            """
            | String Column  | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string1        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            | string2        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string1",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.Contains(collection)).IsTrue();
        table.ShouldContain(collection);
    }

    [Test]
    public async Task TableShouldNotContainCollection()
    {
        var table = Table.Parse(
            """
            | String Column  | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string1        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            | string2        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.Contains(collection)).IsFalse();
        await Assert.That(() => table.ShouldContain(collection)).Throws<TableValidationException>();
    }

    [Test]
    public async Task TableShouldBeSubsetOfCollection()
    {
        var table = Table.Parse(
            """
            | String Column  | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string1        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string1",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            },
            new PropertyClass
            {
                StringColumn = "string2",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.IsSubsetOf(collection)).IsTrue();
        table.ShouldBeSubsetOf(collection);
    }

    [Test]
    public async Task TableShouldNotBeSubsetOfCollection()
    {
        var table = Table.Parse(
            """
            | String Column | Int Column | Date Time Column | Date Only Column | Time Only Column | Decimal Column |
            | string        | 5          | 2025-10-25       | 2025-10-24       | 14:12:11         | 1.234          |
            """);

        var collection = new[]
        {
            new PropertyClass
            {
                StringColumn = "string1",
                IntColumn = 5,
                DateTimeColumn = new DateTime(2025, 10, 25),
                DateOnlyColumn = new DateOnly(2025, 10, 24),
                TimeOnlyColumn = new TimeOnly(14, 12, 11),
                DecimalColumn = 1.234m
            }
        };

        await Assert.That(() => table.IsSubsetOf(collection)).IsFalse();
        await Assert.That(() => table.ShouldBeSubsetOf(collection)).Throws<TableValidationException>();
    }

    [Test]
    public async Task CanCreateSetOfSeparatedValues()
    {
        var table = Table.Parse(
            """
            | string_values | int_list |
            | 123,def       | 5, 6, 7  |
            """);

        var value = table.Get<CollectionsClass>();

        await Assert.That(value.StringValues).IsNotNull().And.IsEquivalentTo(["123", "def"]);
        await Assert.That(value.IntList).IsEquivalentTo([5, 6, 7]);
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

    private class CollectionsClass
    {
        public string[]? StringValues { get; set; }

        public List<int> IntList { get; set; } = [];
    }
}
