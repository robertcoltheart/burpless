using Burpless.Tables.Comparison;
using Burpless.Tables.Validation;

namespace Burpless.Tests.Tables.Validation;

public class ValidatorComparerTests
{
    [Test]
    public async Task CanValidateSimpleTable()
    {
        var table = Table.Validate<Model>(v => v
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value"));

        var data = new Model
        {
            IntValue = 1,
            StringValue = "value"
        };

        var comparer = new ValidatorComparer<Model>();

        var results = comparer.Compare(table, [data]).ToArray();

        await Assert.That(results).Count().IsEqualTo(1);
        await Assert.That(results.Where(x => x.Type == ComparisonType.Match)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task InvalidModelReturnsMismatch()
    {
        var table = Table.Validate<Model>(v => v
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value"));

        var data = new Model
        {
            IntValue = 1,
            StringValue = "wrong value"
        };

        var comparer = new ValidatorComparer<Model>();

        var results = comparer.Compare(table, [data]).ToArray();

        await Assert.That(results).Count().IsEqualTo(1);
        await Assert.That(results.Where(x => x.Type == ComparisonType.Additional)).Count().IsEqualTo(1);
    }

    private class Model
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; } = null!;
    }
}
