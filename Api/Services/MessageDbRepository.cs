using Api.Data.Entity;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Api.Services
{
    /// <summary>
    /// Управление сообщениями в бд
    /// </summary>
    /// <param name="db"></param>
    public class MessageDbRepository(AppDbContext db) : IMessageRepository
    {
        public async Task<IReadOnlyList<MessageDTO>> GetMessages(int skip, int count)
        {
            return (await db.Messages.AsNoTracking().Skip(skip).Take(count).Select(x => (MessageDTO)x).ToListAsync()).AsReadOnly();
        }

        public async Task MessageDelivered(Message message)
        {
            await db.Messages
                   .Where(u => u.Id == message.Id)
                   .ExecuteUpdateAsync(s =>
                       s.SetProperty(u => u.DateRead, u => DateTime.Now));
        }

        public async Task<Message?> SaveMessage(MessageDTO message, User user)
        {
            var savedMessage = db.Messages.Add(new Message
            {
                Text = message.Message,
                DateSend = DateTime.Now,
                FromId = message.From.Id,
                To = user
            }).Entity;

            await db.SaveChangesAsync();

            return savedMessage;
        }
    }
}