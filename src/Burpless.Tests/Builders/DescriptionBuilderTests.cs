using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class DescriptionBuilderTests
{
    [Test]
    public async Task CanSetNameViaBuilder()
    {
        var builder = new DescriptionBuilder<object>()
            .Named("ScenarioName");

        await Assert.That(builder.Details.Name).IsEqualTo("ScenarioName");
    }

    [Test]
    public async Task CanSetDescription()
    {
        var builder = new DescriptionBuilder<object>()
            .DescribedBy("MyDescription");

        await Assert.That(builder.Details.Description).IsEqualTo("MyDescription");
    }

    [Test]
    public async Task CanSetTags()
    {
        var builder = new DescriptionBuilder<object>()
            .WithTags("tag1", "tag2");

        await Assert.That(builder.Details.Tags).Contains("tag1");
        await Assert.That(builder.Details.Tags).Contains("tag2");
    }
}
