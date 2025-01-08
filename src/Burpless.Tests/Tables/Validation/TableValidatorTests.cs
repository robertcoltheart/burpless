using Burpless.Tables.Validation;

namespace Burpless.Tests.Tables.Validation;

public class TableValidatorTests
{
    [Test]
    [Arguments(true, "IntValue", 1, "value")]
    [Arguments(true, "StringValue", 1, "value")]
    [Arguments(true, "wrong column", 1, "value")]
    [Arguments(false, "IntValue", 5, "value")]
    [Arguments(false, "StringValue", 1, "wrong value")]
    public async Task CanVerifyModelUsingTableValidator(bool equals, string column, int intValue, string stringValue)
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Model
        {
            IntValue = intValue,
            StringValue = stringValue
        };

        var result = validator.IsValid(column, model, out _);

        await Assert.That(result).IsEqualTo(equals);
    }

    [Test]
    public void WrongTypeToValidateThrows()
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Dictionary<string, string>();

        Assert.Throws(() => validator.IsValid("column", model, out _));
    }

    [Test]
    [Arguments("IntValue", "1")]
    [Arguments("StringValue", "value")]
    public async Task ValueIsParsedOnValidation(string column, string expected)
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Model
        {
            IntValue = 1,
            StringValue = "value"
        };

        validator.IsValid(column, model, out var parsed);

        await Assert.That(parsed).IsEqualTo(expected);
    }

    [Test]
    [Arguments(true, "IntValue")]
    [Arguments(false, "Int")]
    [Arguments(false, "String")]
    public async Task CanUseCustomColumnNameToValidateInvalidModel(bool equals, string column)
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn("Int", x => x.IntValue, x => x == 1)
            .WithColumn("String", x => x.StringValue, x => x == "value");

        var invalidModel = new Model
        {
            IntValue = 5,
            StringValue = "wrong"
        };

        var valid = validator.IsValid(column, invalidModel, out _);

        await Assert.That(valid).IsEqualTo(equals);
    }

    private class Model
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; }
    }
}
