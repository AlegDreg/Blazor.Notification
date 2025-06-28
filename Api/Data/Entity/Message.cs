using Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entity
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int FromId { get; set; }
        [ForeignKey(nameof(FromId))]
        public User? From { get; set; }

        public int ToId { get; set; }
        [ForeignKey(nameof(ToId))]
        public User? To { get; set; }

        public required string Text { get; set; }
        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime DateSend { get; set; }
        /// <summary>
        /// Дата прочтения (если прочли)
        /// </summary>
        public DateTime? DateRead { get; set; }

        public static explicit operator MessageDTO(Message message)
        {
            ArgumentNullException.ThrowIfNull(message.From);
            ArgumentNullException.ThrowIfNull(message.To);

            return new MessageDTO
            {
                Message = message.Text,
                FromLogin = message.From.Login,
                ToLogin = message.To.Login,
                DateTime = message.DateSend,
                Id = message.Id,
                Readed = message.DateRead != null
            };
        }
    }
}