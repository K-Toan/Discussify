using Microsoft.EntityFrameworkCore;
using RazorClient.Data;
using RazorClient.Models;

namespace RazorClient.Services
{
    public class ChatService
    {
        private readonly OfflinePostDbContext _context;

        public ChatService(OfflinePostDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(int userId, string userName, int communityId, string communityName, string message)
        {
            var chatMessage = new ChatMessage
            {
                UserId = userId,
                UserName = userName,
                CommunityId = communityId,
                CommunityName = communityName,
                Message = message,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessageDto>> GetMessagesForCommunityAsync(int communityId)
        {
            return await _context.ChatMessages
                .Where(m => m.CommunityId == communityId)
                .OrderBy(m => m.SentAt)
                .Select(m => new ChatMessageDto
                {
                    UserId = m.UserId,
                    UserName = m.UserName,
                    CommunityId = communityId,
                    CommunityName = m.UserName,
                    Message = m.Message,
                    SentAt = m.SentAt
                })
                .ToListAsync();
        }
    }
}
