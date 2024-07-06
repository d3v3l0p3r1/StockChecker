using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StockCollector.Extensions;
using StockCollector.ReditApi.Abstractions;
using StockCollector.ReditApi.Implementations;

public class Program
{
    public static async Task Main()
    {
        var serviceProvider = BuildApplication();

        using var scope = serviceProvider.CreateScope();
        var reditProcessor = scope.ServiceProvider.GetService<IRedditProcessor>();
        await reditProcessor.ProcessAsync(new DateOnly(2023, 4, 1), new DateOnly(2023, 7, 1));
    }

    public static ServiceProvider BuildApplication()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IRedditProcessor, RedditProcessor>();
        serviceCollection.AddTransient<IRedditClient, RedditClient>();
        serviceCollection.AddTransient<IRepository, Repository>();

        serviceCollection.AddReditClient();

        serviceCollection.AddLogging(loggingBuilder =>
        {
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

            loggingBuilder.AddSerilog(dispose: true);
        });

        return serviceCollection.BuildServiceProvider();
    }
}

