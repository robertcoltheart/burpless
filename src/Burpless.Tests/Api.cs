using Burpless.Builders;

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

    [Fact(Explicit = true)]
    public async Task AccountIsInCredit() => await Scenario.For<Context>()
        .Feature(feature)
        .Given(x => x.TheAccountIsInCredit())
        .When(x => x.MoneyIsTakenOutOfTheATM())
        .Then(x => x.EnsureTheAccountIsDebited());

    [Fact(Explicit = true)]
    public Task AccountIsValidUsingGherkinTable() => Scenario.For<Context>()
        .Feature(feature)
        .Given(x => x.TheAccountIsInCredit())
        .When(x => x.MoneyIsTakenOutOfTheATM())
        .Then(x => x.EnsureTheAccountIsDebited())
        .And(x => x.TheFollowingDataIsReceived(
            """
            | Account Id | Balance |
            | 12345      | 123.45  |
            """));

    [Fact(Explicit = true)]
    public Task AccountIsValidUsingObject() => Scenario.For<Context>()
        .Feature(feature)
        .Given(x => x.TheAccountIsInCredit())
        .When(x => x.MoneyIsTakenOutOfTheATM())
        .Then(x => x.EnsureTheAccountIsDebited())
        .And(x => x.TheFollowingDataIsReceived(Table.From(new { AccountId = 12345, Balance = 123.45m })));

    [Fact(Explicit = true)]
    public Task AccountIsValidUsingTable() => Scenario.For<Context>()
        .Feature(feature)
        .Given(x => x.TheAccountIsInCredit())
        .When(x => x.MoneyIsTakenOutOfTheATM())
        .Then(x => x.EnsureTheAccountIsDebited())
        .And(x => x.TheFollowingDataIsReceived(Table
            .WithColumns("AccountId", "Balance")
            .AddRow(12345, 123.45m)));

    private class Context
    {
        private object? data;

        public void TheAccountIsInCredit()
        {
        }

        public Task MoneyIsTakenOutOfTheATM()
        {
            data = new object();

            return Task.CompletedTask;
        }

        public Task EnsureTheAccountIsDebited()
        {
            return Task.CompletedTask;
        }

        public Task TheFollowingDataIsReceived(Table table)
        {
            TableAssert.Equivalent(table, data);

            return Task.CompletedTask;
        }
    }
}
