namespace Burpless.Tests;

public class ScenarioTests
{
    [Fact]
    public void ScenarioNameDeterminedFromMethod()
    {
        var scenario = Scenario.For<object>();

        Assert.NotNull(scenario);
        Assert.Equal(nameof(ScenarioNameDeterminedFromMethod), scenario.Details.Name);
    }
}
