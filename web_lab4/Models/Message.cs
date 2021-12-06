namespace web_lab4.Models
{
    public enum MessageType
    {
        Text,
        Photo
    }

    public class Message
    {
        private bool? IsPrivate { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }
    }
}