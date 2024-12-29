namespace Burpless.Tests;

public class ScenarioTests
{
    [Test]
    public async Task ScenarioNameDeterminedFromMethod()
    {
        var scenario = Scenario.For<object>();

        await Assert.That(scenario).IsNotNull();
        await Assert.That(scenario.Details.Name).IsEqualTo(nameof(ScenarioNameDeterminedFromMethod));
    }
}
