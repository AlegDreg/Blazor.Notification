namespace Shared
{
    public class MessageDTO
    {
        public int Id { get; set; } 
        public required string FromLogin { get; set; }
        public required string ToLogin { get; set; }
        public required string Message { get; set; }
        public required DateTime DateTime { get; set; }
        public required bool Readed { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}