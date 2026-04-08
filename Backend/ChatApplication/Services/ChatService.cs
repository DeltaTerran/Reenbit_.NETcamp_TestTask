using ChatApplication.ContextDB;
using ChatApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDBContext _dbContext;
        public ChatService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<List<ChatMessage>> GetRecentMessagesAsync(int count = 50)=>
              await _dbContext.messages
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(count)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync();

        public async Task<ChatMessage> SaveMessageAsync(string userName, string text, string? sentiment = null)
        {
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
