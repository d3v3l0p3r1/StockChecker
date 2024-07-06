using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.RateLimit;
using StockCollector.ReditApi.Abstractions;
using StockCollector.ReditApi.Implementations;
using System.Net;

namespace StockCollector.Extensions
{
    public static class RedditClientExtensions
    {
        public static IServiceCollection AddReditClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IRedditClient, RedditClient>()
             .AddPolicyHandler(GetRpsPolicy(serviceCollection));
            return serviceCollection;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRpsPolicy(IServiceCollection serviceCollection)
        {
            var rateLimitPolicy = Policy.RateLimitAsync<HttpResponseMessage>(100, TimeSpan.FromSeconds(10));

            var rateLimitRetryPolicy = Policy.Handle<RateLimitRejectedException>()
                .WaitAndRetryAsync(retryCount: 3, _ => TimeSpan.FromSeconds(1))
                .WrapAsync(rateLimitPolicy);

            var tooManyRequestPolicy = Policy<HttpResponseMessage>
                .HandleResult(res => res.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: (i, e, ctx) =>
                    {
                        return TimeSpan.FromSeconds(10 * i);
                    },
                    onRetryAsync: async (e, ts, i, ctx) =>
                    {
                        var logger = serviceCollection.BuildServiceProvider().GetService<ILogger<IRedditClient>>();

                        logger.LogError($"Too many requests (429) RETRY {i}");
                    });

            var wrap = tooManyRequestPolicy.WrapAsync(rateLimitRetryPolicy);

            return wrap;
        }
    }
}
