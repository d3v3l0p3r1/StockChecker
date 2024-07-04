using Newtonsoft.Json;

namespace StockCollector.ReditApi.Dtos
{
    public class ResponseData
    {
        [JsonProperty("no_of_comments")]
        public long NumberOfComments { get; set; }

        [JsonProperty("sentiment")]
        public string Sentiment { get; set; }

        [JsonProperty("sentiment_score")]
        public double? SentimentScore { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }
    }
}
