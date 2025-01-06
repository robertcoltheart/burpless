namespace Burpless.Tests;

public class TableValidatorTests
{
    [Test]
    public async Task CanValidateTableWithConditions()
    {
        var table = Table.Parse(
            """
            | String Column | Bool Column | int Column |
            | value         | true        | 123        |
            """);

        var validator = new TableValidator<PropertyClass>();
        validator.WithColumn(x => x.StringColumn, x => x == "value");
        validator.WithColumn(x => x.BoolColumn, x => x == true);
        validator.WithColumn(x => x.IntColumn, x => x > 5);

        var result = validator.IsValid(table);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task CanValidateTableWithDataSet()
    {
        var table = Table.Parse(
            """
            | String Column | Bool Column | int Column |
            | value         | true        | 123        |
            """);

        var data = new PropertyClass
        {
            StringColumn = "value",
            BoolColumn = true,
            IntColumn = 123
        };

        var result = Table.IsValid(table, data);

        await Assert.That(result).IsTrue();
    }

    private class PropertyClass
    {
        public string StringColumn { get; set; }

        public bool BoolColumn { get; set; }

        public int IntColumn { get; set; }
    }
}
