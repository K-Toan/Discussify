namespace RazorClient.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CommunityId { get; set; }
        public string CommunityName { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class ChatMessageDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CommunityId { get; set; }
        public string CommunityName { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
