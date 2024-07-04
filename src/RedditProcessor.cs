using StockCollector.ReditApi.Abstractions;

public class RedditProcessor
{
    private readonly IRepository _repository;
    private readonly IReditClient _reditClient;

    public RedditProcessor(IRepository repository, IReditClient reditClient)
    {
        _repository = repository;
        _reditClient = reditClient;
    }

    public async Task ProcessAsync(DateOnly startDate, DateOnly endDate)
    {
        while (startDate < endDate)
        {
            var tickers = await _reditClient.GetData(startDate);

            foreach (var ticker in tickers)
            {
                var tickerRecord = new TickerRecord 
                { 
                    Name = ticker.Ticker, 
                    NumberOfComments = ticker.NumberOfComments, 
                    Sentiment = ticker.Sentiment, 
                    SentimentScore = ticker.SentimentScore 
                };

                _repository.SaveTicker(tickerRecord.Name);
                _repository.SaveRecord(tickerRecord);
            }

            startDate = startDate.AddDays(1);
        }

        // Display the mocked data collections
        DisplayDataCollections();

        // Fake call to store both collections in a pretend database
        _repository.StoreInDatabase();
    }

    void DisplayDataCollections()
    {
        Console.WriteLine("\nMocked Data Collections - Tickers:");
        foreach (var tuple in _repository.Tickers)
        {
            Console.WriteLine($"Ticker: {tuple.Key}, Records Count: {tuple.Value}");
        }

        Console.WriteLine("\nMocked Data Collections - Records:");
        foreach (var tuple in _repository.TickerRecords)
        {
            Console.WriteLine($"Ticker: {tuple.Name}, Sentiment: {tuple.Sentiment}, Comments: {tuple.NumberOfComments}, Score: {tuple.SentimentScore}");
        }
    }
}

