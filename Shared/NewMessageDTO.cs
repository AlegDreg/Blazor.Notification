namespace Shared
{
    public class NewMessageDTO
    {
        public required string FromLogin { get; set; }
        public required string ToLogin { get; set; }
        public required string Message { get; set; }
    }
}