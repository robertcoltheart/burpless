using Burpless.Tables;

namespace Burpless.Tests.Tables;

public class RowComparerTests
{
    [Test]
    public async Task ClassValuesAreEqualToRow()
    {
        var comparer = new RowComparer<ClassWithProperties>(["Int value", "string-value", "boolvalue"]);

        var item = new ClassWithProperties
        {
            IntValue = 1,
            StringValue = "string-value",
            BoolValue = true,
        };

        var result = comparer.Equivalent(item, ["1", "string-value", "true"]);

        await Assert.That(result).IsTrue();
    }

    private class ClassWithProperties
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; }

        public bool BoolValue { get; set; }
    }
}
