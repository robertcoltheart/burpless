using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class ScenarioGroupingBuilderTests
{
    [Fact]
    public void SetsScenarioNameViaConstructor()
    {
        var builder = new ScenarioGroupingBuilder<object>("MyName");

        Assert.Equal("MyName", builder.Details.Name);
    }

    [Fact]
    public void CanSetFeature()
    {
        var feature = new Feature();
        
        var builder = new ScenarioGroupingBuilder<object>("name")
            .Feature(feature);

        Assert.Same(feature, builder.Details.Feature);
    }
}
