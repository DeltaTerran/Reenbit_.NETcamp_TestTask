
using Azure;
using Azure.AI.TextAnalytics;

namespace ChatApplication.Services
{
    public class AzureSentimentService : ISentimentService
    {
        private readonly TextAnalyticsClient _client;
        public AzureSentimentService(IConfiguration configuration)
        {
            var endpoint = configuration["Azure:Language:Endpoint"];
            var key = configuration["Azure:Language:Key"];

            if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException("Azure Language configuration is missing.");
            }

            _client = new TextAnalyticsClient(
                new Uri(endpoint),
                new AzureKeyCredential(key));
        }
        public async Task<string?> AnalyzeAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var response = await _client.AnalyzeSentimentAsync(text);
            return response.Value.Sentiment.ToString();
        }
    }
}
