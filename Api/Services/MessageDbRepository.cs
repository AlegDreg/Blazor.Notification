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
            return (await db.Messages
                .Include(x => x.From).Include(x => x.To)
                .OrderByDescending(x => x.DateSend)
                .AsNoTracking()
                .Skip(skip).Take(count)
                .Select(x => (MessageDTO)x)
                .ToListAsync())
                .AsReadOnly();
        }

        public async Task MessageDelivered(Message message)
        {
            message.DateRead = DateTime.Now;
            db.Messages.Update(message);
            await db.SaveChangesAsync();
        }

        public async Task MessageDelivered(int messageId)
        {
            await db.Messages
                   .Where(u => u.Id == messageId)
                   .ExecuteUpdateAsync(s =>
                       s.SetProperty(u => u.DateRead, u => DateTime.Now));
        }

        public async Task<Message?> SaveMessage(NewMessageDTO message, User fromUser, User toUser)
        {
            var savedMessage = db.Messages.Add(new Message
            {
                Text = message.Message,
                DateSend = DateTime.Now,
                FromId = fromUser.Id,
                To = toUser
            }).Entity;

            await db.SaveChangesAsync();

            return savedMessage;
        }
    }
}