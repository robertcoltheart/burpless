using Burpless.Builders;

namespace Burpless.Tests.Builders;

public class DescriptionBuilderTests
{
    [Fact]
    public void CanSetNameViaBuilder()
    {
        var builder = new DescriptionBuilder<object>()
            .Name("ScenarioName");

        Assert.Equal("ScenarioName", builder.Details.Name);
    }

    [Fact]
    public void CanSetDescription()
    {
        var builder = new DescriptionBuilder<object>()
            .Description("MyDescription");

        Assert.Equal("MyDescription", builder.Details.Description);
    }

    [Fact]
    public void CanSetTags()
    {
        var builder = new DescriptionBuilder<object>()
            .Tags("tag1", "tag2");

        Assert.Contains(builder.Details.Tags, ["tag1", "tag2"]);
    }
}
