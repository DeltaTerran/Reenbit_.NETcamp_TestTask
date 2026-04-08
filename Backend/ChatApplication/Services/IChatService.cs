using ChatApplication.Models;

namespace ChatApplication.Services
{
    public interface IChatService
    {
        Task<ChatMessage> SaveMessageAsync(string userName, string text, string? sentiment = null);
        Task<List<ChatMessage>> GetRecentMessagesAsync(int count = 50);
    }
}
