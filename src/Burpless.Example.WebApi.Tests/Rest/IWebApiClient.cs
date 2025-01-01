namespace Burpless.Example.WebApi.Tests.Rest;

public interface IWebApiClient
{
    Task<Weather[]?> GetWeatherForecast();

    Task GetInvalidRoute();
}
