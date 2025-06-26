namespace Shared
{
    public class MessageDTO
    {
        public required UserDTO From { get; set; }
        public required string ToLogin { get; set; }
        public required string Message { get; set; }
    }
}