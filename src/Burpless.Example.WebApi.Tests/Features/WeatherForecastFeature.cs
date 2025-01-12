using Burpless.Example.WebApi.Tests.Contexts;
using Burpless.Example.WebApi.Tests.Rest;

namespace Burpless.Example.WebApi.Tests.Features;

public class WeatherForecastFeature
{
    private readonly Feature feature = Feature.Named("Weather forecast")
        .WithDescription(
            """
            Various tests against the weather forecast endpoint.

            We can add more descriptive text here as a raw string.
            """)
        .WithBackground<ServerContext>(background => background
            .Given(x => x.TheServerIsRunning()));

    [Test]
    public Task WeatherForecastCanBeFetched() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .Name("Weather forecast is fetched")
        .Description("When the server is running, it should allow calls that will return weather forecasts")
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.ThereShouldBeValidWeatherData())
        .And(x => x.MoreThanOneForecastExists())
        .And(x => x.NoErrorsWereEncountered());

    [Test]
    public Task WeatherForecastIsFetchedWhenServerIsRunning() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .Given(x => x.TheClientTimeoutIs(TimeSpan.FromSeconds(10)))
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.ThereShouldBeValidWeatherData())
        .And<ServerContext>(x => x.TheServerShouldStillBeRunning())
        .And(x => x.NoErrorsWereEncountered());

    [Test]
    public Task InvalidRouteIsCaught() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .When(x => x.AnInvalidRouteIsCalled())
        .Then<ServerContext>(x => x.TheServerIsRunning())
        .And(x => x.AnExceptionWasThrown());

    [Test]
    public Task WithTableData() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.TheFollowingDataIsReturned(
            """
            | TemperatureC | Summary  |
            | 25           | Freezing |
            | 26           | Bracing  |
            """))
        .And(x => x.TheFollowingDataIsReturned(Table.From(
            new Weather { TemperatureC = 25, Summary = "Freezing", Date = new DateOnly(2024, 12, 25), TemperatureF = 76 },
            new Weather { TemperatureC = 26, Summary = "Bracing", Date = new DateOnly(2024, 12, 26), TemperatureF = 78 })))
        .And(x => x.TheFollowingDataIsReturned(Table
            .WithColumns("TemperatureC", "Summary")
            .AddRow("25", "Freezing")
            .AddRow("26", "Bracing")));

    [Test]
    public Task WithTableValidation() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.TheFollowingDataIsReturned(Table.Validate<Weather>(validator => validator
            .WithColumn(c => c.TemperatureC, c => c > 20)
            .WithColumn(c => c.TemperatureF, c => c > 70))));
}
