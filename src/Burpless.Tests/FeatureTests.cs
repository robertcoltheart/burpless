namespace Burpless.Tests;

public class FeatureTests
{
    [Test]
    public async Task CanCreateNamedFeature()
    {
        var feature = Feature.Named("name");

        await Assert.That(feature.Name).IsEqualTo("name");
    }

    [Test]
    public async Task FeatureContainsDescription()
    {
        var feature = Feature.Named("name")
            .DescribedBy("my description");

        await Assert.That(feature.Description).IsEqualTo("my description");
    }

    [Test]
    public async Task FeatureContainsTags()
    {
        var feature = Feature.Named("name")
            .WithTags("tag1", "tag2");

        await Assert.That(feature.Tags).Contains("tag1")
            .And.Contains("tag2");
    }

    [Test]
    public async Task CanCreateStepsInFeature()
    {
        var feature = Feature.Named("name")
            .Background<Context>(background => background
                .Given(x => x.Given()));

        await Assert.That(feature.Steps).IsNotEmpty();
    }

    [Test]
    public async Task CanCreateNamedStepInFeature()
    {
        var feature = Feature.Named("name")
            .Background<Context>(background => background
                .Given("some action", x => x.Given()));

        await Assert.That(feature.Steps).IsNotEmpty();
    }

    [Test]
    public async Task FeaturesWithSameNameAreEqual()
    {
        var feature1 = Feature.Named("name");
        var feature2 = Feature.Named("name");

        await Assert.That(feature1).IsEqualTo(feature2);
    }

    private class Context
    {
        public void Given()
        {
        }
    }
}
