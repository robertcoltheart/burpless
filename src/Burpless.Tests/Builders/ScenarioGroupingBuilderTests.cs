using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ScenarioGroupingBuilderTests
{
    [Test]
    public async Task SetsScenarioNameViaConstructor()
    {
        var builder = new ScenarioGroupingBuilder<object>("MyName");

        await Assert.That(builder.Details.Name).IsEqualTo("MyName");
    }

    [Test]
    public async Task CanSetFeature()
    {
        var feature = Feature.Named("feature");
        
        var builder = new ScenarioGroupingBuilder<object>("name")
            .ForFeature(feature);

        await Assert.That(builder.Details.Feature).IsEqualTo(feature);
    }
}
