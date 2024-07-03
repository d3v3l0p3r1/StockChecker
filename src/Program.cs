using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

    // Mocked data collections to store ticker information and records
    var tickerCollection = new List<Tuple<string, int>>();
    var recordCollection = new List<Tuple<string, string, long>>();

for (int month = 4; month <= 6; month++)
{
    string apiUrlBase = "https://tradestie.com/api/v1/apps/reddit?date=2023-";
    // Construct the API URL base for the current month
    string apiUrl = $"{apiUrlBase}{month:D2}-";

    // Loop through 30 days
    for (int day = 1; day <= 30; day++)
    {
        // Construct the full API URL for each day
        string fullUrl = apiUrl + day.ToString("D2");

        // Perform the API request
        var responseData = await GetApiData(fullUrl);

        // Parse and process the response
        ProcessApiData(responseData, tickerCollection, recordCollection);
    }
}

// Display the mocked data collections
DisplayDataCollections(tickerCollection, recordCollection);

    // Fake call to store both collections in a pretend database
    StoreInDatabase(tickerCollection, recordCollection);

static async Task<string> GetApiData(string apiUrl)
{
    using (var httpClient = new HttpClient())
    {
        var response = await httpClient.GetStringAsync(apiUrl);
        return response;
    }
}

static void ProcessApiData(string responseData, List<Tuple<string, int>> tickerCollection, List<Tuple<string, string, long>> recordCollection)
{
    // Deserialize JSON response
    var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseData);

    // Loop through each data entry
    foreach (var entry in data)
    {
        var ticker = (string)entry.GetValueOrDefault("ticker");
        var sentiment = (string)entry.GetValueOrDefault("sentiment");
        var noOfComments = (long)entry.GetValueOrDefault("no_of_comments");

        // Extract relevant information
        // Mocked data collection insertion for each ticker
        SaveTicker(ticker, tickerCollection);

        // Mocked data collection insertion for each record with foreign key
        SaveRecord(ticker, sentiment, noOfComments, recordCollection);
    }
}

static void SaveTicker(string ticker, List<Tuple<string, int>> tickerCollection)
{
    tickerCollection.Add(new Tuple<string, int>(ticker, 0));
    Console.WriteLine($"Saved Ticker: {ticker}");
}

static void SaveRecord(string ticker, string sentiment, long noOfComments, List<Tuple<string, string, long>> recordCollection)
{
    recordCollection.Add(new Tuple<string, string, long>(ticker, sentiment, noOfComments));
    Console.WriteLine($"Saved Record: Ticker - {ticker}, Sentiment - {sentiment}, Comments - {noOfComments}");
}

static void DisplayDataCollections(List<Tuple<string, int>> tickerCollection, List<Tuple<string, string, long>> recordCollection)
{
    Console.WriteLine("\nMocked Data Collections - Tickers:");
    foreach (var tuple in tickerCollection)
    {
        Console.WriteLine($"Ticker: {tuple.Item1}, Records Count: {tuple.Item2}");
    }

    Console.WriteLine("\nMocked Data Collections - Records:");
    foreach (var tuple in recordCollection)
    {
        Console.WriteLine($"Ticker: {tuple.Item1}, Sentiment: {tuple.Item2}, Comments: {tuple.Item3}");
    }
}

// Fake call to store both collections in a pretend database
static void StoreInDatabase(List<Tuple<string, int>> tickerCollection, List<Tuple<string, string, long>> recordCollection)
{
    Console.WriteLine("\nFake Call: Storing both data collections in a pretend database.");
}

