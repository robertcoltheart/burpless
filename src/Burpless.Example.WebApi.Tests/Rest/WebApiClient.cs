using System.Net.Http.Json;

namespace Burpless.Example.WebApi.Tests.Rest;

public class WebApiClient(HttpClient client) : IWebApiClient
{
    public Task<Weather[]?> GetWeatherForecast()
    {
        return client.GetFromJsonAsync<Weather[]>("/weatherforecast");
    }

    public async Task GetInvalidRoute()
    {
        var response = await client.GetAsync("/invalid");

        response.EnsureSuccessStatusCode();
    }
}
