using Burpless.Tables;

namespace Burpless.Tests.Tables;

public class DifferenceComparerTests
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

        var comparer = new DifferenceComparer<Model>();
        var results = comparer.GetComparisons(table, items).ToArray();

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

        var table = Table.WithColumns("Id", "String", "DateTime")
            .AddRow("1", "value", "2020-12-10")
            .AddRow("3", "value3", "2020-12-10");

        var comparer = new DifferenceComparer<Model>();
        var results = comparer.GetComparisons(table, items).ToArray();

        var match = results.FirstOrDefault(x => x.Type == ComparisonType.Match);
        var missing = results.FirstOrDefault(x => x.Type == ComparisonType.Missing);
        var additional = results.FirstOrDefault(x => x.Type == ComparisonType.Additional);

        await Assert.That(match).IsNotNull();
        await Assert.That(missing).IsNotNull();
        await Assert.That(additional).IsNotNull();
    }

    private class Model
    {
        public int Id { get; set; }

        public string String { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
