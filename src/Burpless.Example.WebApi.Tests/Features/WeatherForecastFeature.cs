﻿using Burpless.Example.WebApi.Tests.Contexts;
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
        .WithBackground<WeatherContext>(background => background
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
        .And(x => x.TheServerShouldStillBeRunning())
        .And(x => x.NoErrorsWereEncountered());

    [Test]
    public Task InvalidRouteIsCaught() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .When(x => x.AnInvalidRouteIsCalled())
        .Then(x => x.TheServerIsRunning())
        .And(x => x.AnExceptionWasThrown());

    [Test]
    public Task WithTableData() => Scenario.For<WeatherContext>()
        .Feature(feature)
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.TheFollowingDataIsReturned(
            """
            | Weather | Something |
            | f       | 123       |
            """))
        .And(x => x.TheFollowingDataIsReturned(Table.From(
            new Weather { TemperatureC = 10 },
            new Weather { TemperatureC = 11 })))
        .And(x => x.TheFollowingDataIsReturned(Table
            .WithColumns("Weather", "Something")
            .AddRow("f", "123")
            .AddRow("weather", "123")));
}
