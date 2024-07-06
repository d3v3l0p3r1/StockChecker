using Newtonsoft.Json;
using StockCollector.ReditApi.Abstractions;
using StockCollector.ReditApi.Dtos;

namespace StockCollector.ReditApi.Implementations
{
    public class RedditClient : IRedditClient
    {
        private readonly HttpClient _httpClient;
        private string apiUrlBase = $"https://tradestie.com/api/v1/apps/reddit?date=";

        public RedditClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResponseData>> GetData(DateOnly date)
        {
            string url = $"{apiUrlBase}{date:yyyy-MM-dd}";
            var response = await _httpClient.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<List<ResponseData>>(response);

            return data;
        }
    }
}
