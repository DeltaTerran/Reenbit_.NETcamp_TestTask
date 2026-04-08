using ChatApplication.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(message))
                    return;

                var saved = await _chatService.SaveMessageAsync(user, message);

                await Clients.All.SendAsync("ReceiveMessage", saved.UserName, saved.Text, saved.Sentiment, saved.CreatedAtUtc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== SendMessage ERROR ===");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task LoadHistory()
        {
            try
            {
                var messages = await _chatService.GetRecentMessagesAsync(50);

                await Clients.Caller.SendAsync("ReceiveHistory", messages.Select(m => new
                {
                    user = m.UserName,
                    text = m.Text,
                    sentiment = m.Sentiment,
                    createdAtUtc = m.CreatedAtUtc
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== LoadHistory ERROR ===");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}