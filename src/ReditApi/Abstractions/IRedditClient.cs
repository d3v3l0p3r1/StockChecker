using StockCollector.ReditApi.Dtos;

namespace StockCollector.ReditApi.Abstractions
{
    public interface IRedditClient
    {
        Task<List<ResponseData>> GetData(DateOnly date);
    }
}