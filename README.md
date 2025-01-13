# Burpless

[![NuGet](https://img.shields.io/nuget/v/Burpless?style=for-the-badge)](https://www.nuget.org/packages/Burpless) [![License](https://img.shields.io/github/license/robertcoltheart/Burpless?style=for-the-badge)](https://github.com/robertcoltheart/Burpless/blob/master/LICENSE)

Burpless is a behavior-driven development (BDD) framework that can be used to construct and execute features and scenarios.

The framework tests broadly adhere to Gherkin syntax, so if you are used to writing Gherkin, you'll feel at home.
Burpless is framework-agnostic, and you can plug in the test framework of your choice. Examples are given below for the
major frameworks.

In addition, living documentation can also be generated from your test runs, if desired.

See more on the philosophy behind Burpless [here](#philosophy).

## Usage
Install the package from NuGet with `dotnet add package Burpless`.

### Writing your first test
The test below uses xUnit, but you can adopt any framework you choose. Some other framework examples are given at the bottom.

```csharp
public class WeatherFeature
{
    [Fact]
    public Task FetchesTheWeatherForecast() => Scenario.For<WeatherContext>()
        .Given(x => x.TheWeatherServerIsRunning())
        .When(x => x.TheWeatherIsFetched())
        .Then(x => x.WeatherDataShouldBeReturned());
}
```

Note that your test should return `Task`. You can also `async/await` the scenario if you wish.

All scenarios have a context which is the code that actually executes the steps. An example from above is given:

```csharp
public class WeatherContext
{
    public void TheWeatherServerIsRunning()
    {
        // do the server setup step
    }

    public void TheWeatherIsFetched()
    {
        // fetch weather from the API
    }

    public void WeatherDataShouldBeReturned()
    {
        // assert that data was returned from the API
    }
}
```

During the test run, a new context is created for each scenario, so if you want to preserve state between your steps, you can simply use a
field in your context. For example, storing the output of an API call or catching an exception.

### Features and metadata
As with Gherkin, you can add more information about your feature or scenario, as well as provide some background steps that should
be run for each of your scenarios.

Construct a feature in your test class and link it to your scenarios as below:

```csharp
public class WeatherFeature
{
    private readonly Feature feature = Feature.Named("Weather forecast")
        .DescribedBy("As a user, I want to see the weather so that I know whether it's sunny")
        .WithTags("weather", "api")
        .Background<ServerContext>(background => background
            .Given(x => x.TheWeatherServerIsRunning()))
            .And(x => x.TheDatabaseIsReady());

    [Fact]
    public Task FetchesTheWeatherForecasts() => Scenario.For<WeatherContext>()
        .ForFeature(feature)
        .Named("Weather forecast is fetched")
        .DescribedBy("As a client, I can fetch weather from the API so that I know its working")
        .WithTags("api", "client")
        .When(x => x.WeatherIsFetchedFromTheServer())
        .Then(x => x.ThereShouldBeNoExceptions());
}
```

The feature background steps are run before each of your scenarios. It is best practice to
group your scenarios with a feature in a single code file, much like Gherkin.

### Scenario outlines
If you are wanting to run the same scenario with different input parameters, you can simply use the arguments of your
method and provide data using the parameterized feature of your test framework.

In xUnit this would look like:

```csharp
public class WeatherFeature
{
    [Fact]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(10)]
    public Task FetchesTheWeatherForecastForDaysInAdvance(int days) => Scenario.For<WeatherContext>()
        .Given(x => x.TheWeatherServerIsRunning())
        .When(x => x.TheWeatherIsFetchedForDaysAhead(days))
        .Then(x => x.WeatherDataShouldBeReturned());
}
```

### Using contexts
Contexts are the "code-behind" of your scenarios and do most of the heavy lifting when it comes to executing your steps.
Each context is treated as a singleton for the life of a scenario, and you can store test state in the context to assist
in verifying the behavior of your scenario.

In addition, contexts can use dependency injection to inject services.
You can configure your container using the [configuration options](#configuration-options).

To encourage code re-use, you can split your steps into different contexts. There is no limit on the number of contexts
you can use in your scenarios.

An example showing multiple contexts being used is below. In this example, steps related to the server have been moved
to their own context:

```csharp
    [Fact]
    public Task FetchesTheWeatherForecast() => Scenario.For<WeatherContext>()
        .Given<ServerContext>(x => x.TheWeatherServerIsRunning())
        .When(x => x.TheWeatherIsFetched())
        .Then(x => x.WeatherDataShouldBeReturned())
        .And<ExceptionsContext>(x => x.ThereShouldBeNoErrors());
```

### Configuration options
During the test session startup, you can configure Burpless to inject services into your contexts from your dependency
injection container.

You can wire up your container as follows:

```csharp
BurplessSettings.Configure(x => x.UseServiceProvider(myContainer));
```

For example, you can start an ASP.NET Core API, and use the built service container to inject services into your contexts.

### Framework examples
Examples for the major testing frameworks have been given below:

#### xUnit
```csharp
public class WeatherFeature
{
    [Test]
    public Task FetchesTheWeatherForecast() => Scenario.For<WeatherContext>()
        .Given(x => x.TheWeatherServerIsRunning())
        .When(x => x.TheWeatherIsFetched())
        .Then(x => x.WeatherDataShouldBeReturned());
}

public class WeatherContext
{
}

[CollectionDefinition(nameof(ApplicationCollection)]
public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
{
}

public class ApplicationFixture : IDisposable
{
    public WebApplicationFactory<Program> Factory { get; private set; }

    public ApplicationFixture()
    {
        Factory = new WebApplicationFactory<Program>();

        // Allow contexts to be injected with services from the app
        BurplessSettings.Configure(x => x.UseServiceProvider(Factory.Services));
    }
}
```

#### NUnit


#### TUnit
```csharp
public class WeatherFeature
{
    [Test]
    public Task FetchesTheWeatherForecast() => Scenario.For<WeatherContext>()
        .Given(x => x.TheWeatherServerIsRunning())
        .When(x => x.TheWeatherIsFetched())
        .Then(x => x.WeatherDataShouldBeReturned());
}

// ASP.NET Core services can be injected into contexts
private class WeatherContext(IWebHostEnvironment environmnet) : ContextBase
{
    private string? weather;

    public void TheWeatherServerIsRunning()
    {
    }

    public async Task TheWeatherIsFetched()
    {
        weather = await Client.GetStringAsync("/weatherforecast");
    }

    public async Task WeatherDataShouldBeReturned()
    {
        await Assert.That(weather).IsNotNullOrEmpty();
    }
}

public class ContextBase
{
    private WebApplicationFactory<Program> factory;

    protected HttpClient Client { get; private set; }

    [Before(TestSession)]
    public void BeforeSession()
    {
        factory = new WebApplicationFactory<Program>();

        // Allow contexts to be injected with services from the app
        BurplessSettings.Configure(x => x.UseServiceProvider(factory.Services));
    }

    [Before(Test)]
    public void BeforeTest()
    {
        Client = factory.CreateClient();
    }
}

```

## Philosophy
Why use a framework like Burpless instead of a Cucumber-based framework like SpecFlow or Reqnroll?

One of the selling points of Cucumber is that non-technical members of your team or business are able to contribute
feature tests using the natural language constructs in Gherkin. In addition, tests that are written by developers
can also be read by business analysts and the product owners and can be ratified without any knowledge of the underlying
code.

In reality, this rarely happens, and it is almost always the developers that are writing and maintaining feature tests.

As with any project, the best kind of testing occurs when the development team, the QA team, the business analysts and
the business are working closely together. And if developers are going to be writing the tests that are captured as part
of ongoing development, it benefits the team more to use a language that is closer to what developers use.

A framework for behavior testing should focus on the developer experience.

Here are some of the benefits of using a code-first approach to behavior testing:

 - No need to rely on flaky plugins for your IDE that bridge the gap between Gherkin and code-behind source
 - Refactoring scenarios is much simpler and can leverage your in-built refactoring tools
 - Where the framework resembles Gherkin syntax, living documentation can still be generated if desired
 - No tedious mapping of Gherkin language steps to code-behind source
 - Framework-agnostic, and will work with the test framework of your choice (xUnit, NUnit, TUnit)

## Get in touch
Raise an [issue here](https://github.com/robertcoltheart/Burpless/issues).

## Contributing
Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to contribute to this project.

## License
Burpless is released under the [MIT License](LICENSE)
