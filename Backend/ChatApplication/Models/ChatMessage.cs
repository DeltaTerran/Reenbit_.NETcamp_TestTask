namespace ChatApplication.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public string? Sentiment { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
