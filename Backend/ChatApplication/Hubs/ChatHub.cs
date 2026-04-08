
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string message)=>
            await Clients.All.SendAsync("ReceiveMessage", name, message);
    }
}
