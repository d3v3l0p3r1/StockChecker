using StockCollector.ReditApi.Dtos;

namespace StockCollector.ReditApi.Abstractions
{
    public interface IReditClient
    {
        Task<List<ResponseData>> GetData(DateOnly date);
    }
}