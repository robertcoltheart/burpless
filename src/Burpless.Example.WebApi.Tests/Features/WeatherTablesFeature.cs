using Burpless.Example.WebApi.Tests.Contexts;
using Burpless.Example.WebApi.Tests.Rest;

namespace Burpless.Example.WebApi.Tests.Features;

public class WeatherTablesFeature
{
    [Test]
    public Task WithTableData() => Scenario.For<WeatherContext>()
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.TheFollowingDataIsReturned(
            """
            | TemperatureC | Summary  |
            | 3            | Freezing |
            | 4            | Bracing  |
            """))
        .And(x => x.TheFollowingDataIsReturned(Table.From(
            new Weather { TemperatureC = 3, Summary = "Freezing", Date = new DateOnly(2024, 12, 25), TemperatureF = 37 },
            new Weather { TemperatureC = 4, Summary = "Bracing", Date = new DateOnly(2024, 12, 26), TemperatureF = 39 })))
        .And(x => x.TheFollowingDataIsReturned(Table
            .WithColumns("TemperatureC", "Summary")
            .AddRow("3", "Freezing")
            .AddRow("4", "Bracing")));

    [Test]
    public Task WithTableValidation() => Scenario.For<WeatherContext>()
        .When(x => x.TheWeatherForecastIsFetched())
        .Then(x => x.TheFollowingDataIsReturned(Table.Validate<Weather>(validator => validator
            .WithColumn(c => c.TemperatureC, c => c > 2)
            .WithColumn(c => c.TemperatureF, c => c > 35))));
}
