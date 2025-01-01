namespace Burpless.Example.WebApi.Tests.Contexts;

public class ServerContext
{
    private bool running;

    public void TheServerIsRunning()
    {
        running = true;
    }

    public async Task TheServerShouldStillBeRunning()
    {
        await Assert.That(running).IsTrue();
    }
}
