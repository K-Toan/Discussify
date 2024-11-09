using Microsoft.AspNetCore.SignalR;
using RazorClient.Services;

namespace RazorClient.Hubs
{
    public class TalkHub : Hub
    {
        private readonly ChatService _chatService;

        public TalkHub(ChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task SendMessage(int userId, string userName, int communityId, string communityName, string message)
        {
            await _chatService.SaveMessageAsync(userId, userName, communityId, communityName, message);

            await Clients.Group(communityName).SendAsync("ReceiveMessage", userName, message);
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
