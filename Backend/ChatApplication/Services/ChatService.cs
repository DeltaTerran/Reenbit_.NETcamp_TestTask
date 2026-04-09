using ChatApplication.ContextDB;
using ChatApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDBContext _dbContext;
        private readonly ISentimentService _sentimentService;
        public ChatService(AppDBContext dBContext, ISentimentService sentimentService)
        {
            _dbContext = dBContext;
            _sentimentService = sentimentService;
        }
        public async Task<List<ChatMessage>> GetRecentMessagesAsync(int count = 50)=>
              await _dbContext.messages
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(count)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync();

        public async Task<ChatMessage> SaveMessageAsync(string userName, string text, string? sentiment = null)
        {
            sentiment ??= await _sentimentService.AnalyzeAsync(text);
            var entity = new ChatMessage
            {
                UserName = userName.Trim(),
                Text = text.Trim(),
                Sentiment = sentiment,
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.messages.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
