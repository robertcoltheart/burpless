namespace Burpless.Tests;

public class StringExtensionsTests
{
    [Test]
    [Arguments("MethodNameTurnedIntoSentence", "method name turned into sentence")]
    [Arguments("SomethingWith1stNumbering", "something with 1st numbering")]
    [Arguments("ANameStartingWithA", "a name starting with a")]
    [Arguments("WhenABigNumber1234IsInTheMiddle", "when a big number 1234 is in the middle")]
    [Arguments("When_a_method_is_underscored", "when a method is underscored")]
    [Arguments("With__multiple__underscores____here", "with multiple underscores here")]
    [Arguments("WhenIUseMyOwnName", "when I use my own name")]
    [Arguments("WithANumberAtTheEnd100", "with a number at the end 100")]
    [Arguments("WithTheUSGovernment", "with the US government")]
    [Arguments("AnABCAgreement", "an ABC agreement")]
    public async Task CanConvertMethodNameToSentence(string input, string expected)
    {
        await Assert.That(input.ToSentence()).IsEqualTo(expected);
    }
}
