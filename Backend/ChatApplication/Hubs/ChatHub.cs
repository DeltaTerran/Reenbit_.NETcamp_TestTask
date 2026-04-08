
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        public async Task BroadcastMessage(string name, string message)=>
            await Clients.All.SendAsync("ReciveMessage",name, message);
    }
}
