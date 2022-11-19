namespace NetProblem.Tests.Config;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

public class StartupTests
{
    private static Startup GetService() => new();

    [Fact]
    public void ConfigureServices()
    {
        // arrange
        var services = new ServiceCollection();
        var expectedTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.Namespace == "NetProblem.Service.Interfaces")
            .ToList();

        // act
        GetService().ConfigureServices(services);

        // assert
        using var serviceProvider = services.BuildServiceProvider();
        foreach (var expectedType in expectedTypes)
        {
            Assert.NotNull(serviceProvider.GetService(expectedType));
        }
    }
}