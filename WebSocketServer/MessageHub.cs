using Microsoft.AspNetCore.SignalR;

namespace WebSocketServer
{
    public class MessageHub : Hub 
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("RecievedMessage", message); 
        }
    }
}
