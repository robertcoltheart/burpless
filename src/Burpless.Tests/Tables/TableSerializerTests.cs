using Burpless.Tables;

namespace Burpless.Tests.Tables;

public class TableSerializerTests
{
    [Test]
    public async Task CanDeserializeTable()
    {
        var table = Table.WithColumns("Id", "NullableString", "DateTime", "NullableDecimal", "Bool")
            .AddRow("1", "value", "2024-12-25", "123.456", "true");

        var serializer = new TableSerializer();
        var result = serializer.Deserialize<Model>(table).Single();

        await Assert.That(result.Id).IsEqualTo(1);
        await Assert.That(result.NullableString).IsEqualTo("value");
        await Assert.That(result.DateTime).IsEqualTo(new DateTime(2024, 12, 25));
        await Assert.That(result.NullableDecimal).IsEqualTo(123.456m);
        await Assert.That(result.Bool).IsTrue();
    }

    [Test]
    public async Task IgnoresColumnsThatDoNotMatch()
    {
        var table = Table.WithColumns("Id", "wrong 1", "wrong 2", "wrong 3", "Bool")
            .AddRow("1", "value", "2024-12-25", "123.456", "true");

        var serializer = new TableSerializer();
        var result = serializer.Deserialize<Model>(table).Single();

        await Assert.That(result.Id).IsEqualTo(1);
        await Assert.That(result.NullableString).IsNull();
        await Assert.That(result.DateTime).IsDefault();
        await Assert.That(result.NullableDecimal).IsNull();
        await Assert.That(result.Bool).IsTrue();
    }

    private class Model
    {
        public int Id { get; set; }

        public string? NullableString { get; set; }

        public DateTime DateTime { get; set; }

        public decimal? NullableDecimal { get; set; }

        public bool Bool { get; set; }
    }
}
