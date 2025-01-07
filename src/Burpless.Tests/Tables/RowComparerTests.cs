using Burpless.Tables;

namespace Burpless.Tests.Tables;

public class RowComparerTests
{
    [Test]
    [Arguments(true, new[] { "1", "string-value", "true" })]
    [Arguments(false, new[] { "2", "string-value", "true" })]
    [Arguments(false, new[] { "1", "wrong-value", "true" })]
    [Arguments(false, new[] { "1", "string-value", "false" })]
    public async Task ValuesCanBeComparedWithRow(bool equal, string[] values)
    {
        var comparer = new RowComparer<ClassWithProperties>(["IntValue", "StringValue", "BoolValue"]);

        var item = new ClassWithProperties
        {
            IntValue = 1,
            StringValue = "string-value",
            BoolValue = true,
        };

        var result = comparer.Equivalent(item, values);

        await Assert.That(result).IsEqualTo(equal);
    }

    private class ClassWithProperties
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; }

        public bool BoolValue { get; set; }
    }
}
