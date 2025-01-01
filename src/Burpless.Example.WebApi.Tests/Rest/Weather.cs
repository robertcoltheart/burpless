namespace Burpless.Example.WebApi.Tests.Rest;

public class Weather
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}
