namespace ChatApplication.Services
{
    public interface ISentimentService
    {
        Task<string?> AnalyzeAsync(string text);
    }
}
