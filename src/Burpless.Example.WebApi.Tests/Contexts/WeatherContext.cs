using Burpless.Example.WebApi.Tests.Rest;

namespace Burpless.Example.WebApi.Tests.Contexts;

public class WeatherContext(IWebApiClient client) : ServerContext
{
    private Weather[]? weather;

    private TimeSpan? clientTimeout;

    private Exception? exception;

    public async Task TheWeatherForecastIsFetched()
    {
        weather = await client.GetWeatherForecast();
    }

    public async Task AnInvalidRouteIsCalled()
    {
        try
        {
            await client.GetInvalidRoute();
        }
        catch (Exception e)
        {
            exception = e;
        }
    }

    public async Task ThereShouldBeValidWeatherData()
    {
        await Assert.That(weather).IsNotNull();
    }

    public async Task MoreThanOneForecastExists()
    {
        await Assert.That(weather?.Length ?? 0).IsGreaterThan(1);
    }

    public void TheClientTimeoutIs(TimeSpan timeout)
    {
        clientTimeout = timeout;
    }

    public async Task NoErrorsWereEncountered()
    {
        await Assert.That(exception).IsNull();
    }

    public async Task AnExceptionWasThrown()
    {
        await Assert.That(exception).IsNotNull();
    }

    public void TheFollowingDataIsReturned(Table table)
    {
        table.ShouldEqual(weather!);
    }
}
