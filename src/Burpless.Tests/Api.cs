namespace Burpless.Tests;

[Collection(nameof(ApiCollection))]
public class Api(WebApi web)
{
    private readonly Feature feature = Feature
        .Named("Cash machine")
        .WithDescription("Cash machine should give out money")
        .WithTags("atm", "cash")
        .WithBackground<Context>(background => background
            .Given(x => x.TheAccountIsInCredit()));

    /// <summary>
    /// Feature: Cash machine
    /// 
    ///   Scenario: Account is in credit
    ///
    ///   Given the account is in credit
    ///   Then ensure the account is debited
    /// </summary>
    [Fact]
    public void AccountIsInCredit() => Scenario.For<Context>()
        .Feature(feature)
        .Given(c => c.TheAccountIsInCredit())
        .Then(c => c.EnsureTheAccountIsDebited())
        .Execute();

    /// <summary>
    /// Feature: Cash machine
    /// 
    ///   Scenario: Account is in credit
    ///
    ///   Given the account is in credit
    ///   Then ensure the account is debited
    /// </summary>
    [Fact]
    public Task AccountIsValid() => Scenario.For<Context>()
        .Feature(feature)
        .Given(c => c.TheAccountIsInCredit())
        .Then(c => c.EnsureTheAccountIsDebited())
        .ExecuteAsync();

    private class Context
    {
        public void TheAccountIsInCredit()
        {
        }

        public Task EnsureTheAccountIsDebited()
        {
            return Task.CompletedTask;
        }
    }
}
