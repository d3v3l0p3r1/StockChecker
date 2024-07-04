public interface IRepository
{
    IReadOnlyDictionary<string, int> Tickers { get; }
    IReadOnlyCollection<TickerRecord> TickerRecords { get; }

    void SaveRecord(TickerRecord ticker);
    void SaveTicker(string ticker);
    void StoreInDatabase();
}