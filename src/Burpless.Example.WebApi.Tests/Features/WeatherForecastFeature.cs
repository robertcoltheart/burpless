using Burpless.Example.WebApi.Tests.Contexts;
using Burpless.Example.WebApi.Tests.Rest;

namespace Burpless.Example.WebApi.Tests.Features;

public class WeatherForecastFeature
{
    private readonly Feature feature = Feature.Named("Weather forecast")
        .DescribedBy(
            """
            Various tests against the weather forecast endpoint.

            We can add more descriptive text here as a raw string.
            """)
        .Background<ServerContext>(background => background
            .Given(x => x.TheServerIsRunning()));

    [Test]
    public Task WeatherForecastCanBeFetched() => Scenario.For<WeatherContext>()
        .ForFeature(feature)
        .Named("Weather forecast is fetched")
        .DescribedBy("When the server is running, it should allow calls that will return weather forecasts")
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.ThereShouldBeValidWeatherData())
        .And(x => x.MoreThanOneForecastExists())
        .And(x => x.NoErrorsWereEncountered());

    [Test]
    public Task WeatherForecastIsFetchedWhenServerIsRunning() => Scenario.For<WeatherContext>()
        .ForFeature(feature)
        .Given(x => x.TheClientTimeoutIs(TimeSpan.FromSeconds(10)))
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.ThereShouldBeValidWeatherData())
        .And<ServerContext>(x => x.TheServerShouldStillBeRunning())
        .And(x => x.NoErrorsWereEncountered());

    [Test]
    public Task InvalidRouteIsCaught() => Scenario.For<WeatherContext>()
        .ForFeature(feature)
        .When(x => x.AnInvalidRouteIsCalled())
        .Then<ServerContext>(x => x.TheServerIsRunning())
        .And(x => x.AnExceptionWasThrown());
}
