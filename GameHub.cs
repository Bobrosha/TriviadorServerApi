using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TriviadorServerApi
{
    public class GameHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}