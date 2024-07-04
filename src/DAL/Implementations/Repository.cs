using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Repository : IRepository
{
    private readonly Dictionary<string, int> tickerCollection = new Dictionary<string, int>();
    private readonly List<TickerRecord> recordCollection = new List<TickerRecord>();
    private readonly ILogger<Repository> _logger;

    public Repository(ILogger<Repository> logger)
    {
        _logger = logger;
    }

    public IReadOnlyDictionary<string, int> Tickers => tickerCollection;
    public IReadOnlyCollection<TickerRecord> TickerRecords => recordCollection;

    public void SaveTicker(string ticker)
    {
        ref int t = ref CollectionsMarshal.GetValueRefOrNullRef(tickerCollection, ticker);

        if (Unsafe.IsNullRef(ref t))
        {
            tickerCollection.Add(ticker, 1);
        }
        else
        {
            t++;
        }

        _logger.LogInformation($"Saved Ticker: {ticker}");
    }

    public void SaveRecord(TickerRecord ticker)
    {
        recordCollection.Add(ticker);
        _logger.LogInformation($"Saved Record: Ticker - {ticker.Name}, Sentiment - {ticker.Sentiment}, Comments - {ticker.NumberOfComments}, Score - {ticker.SentimentScore}");
    }

    public void StoreInDatabase()
    {
        _logger.LogInformation("\nFake Call: Storing both data collections in a pretend database.");
    }
}

