using Microsoft.AspNetCore.SignalR;

namespace RazorClient.Hubs
{
    public class TalkHub : Hub
    {
        public async Task SendMessage(string communityName, string user, string message)
        {
            await Clients.Group(communityName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinGroup(string communityName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, communityName);
            await Clients.Group(communityName).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the group.");
        }

        public async Task LeaveGroup(string communityName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, communityName);
            await Clients.Group(communityName).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has left the group.");
        }
    }
}
