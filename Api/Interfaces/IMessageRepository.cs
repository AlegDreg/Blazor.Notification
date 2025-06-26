using Api.Data.Entity;
using Shared;

namespace Api.Interfaces
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Отправить сообщение пользователю
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<Message?> SaveMessage(MessageDTO message, User toUser);
        /// <summary>
        /// Получить сообщения
        /// </summary>
        /// <param name="skip">Пропустить кол-во сообщений</param>
        /// <param name="count">Максимум возвращаемых сообщений</param>
        /// <returns></returns>
        Task<IReadOnlyList<MessageDTO>> GetMessages(int skip, int count);
        /// <summary>
        /// Сообщение доставлено
        /// </summary>
        /// <param name="message"></param>
        Task MessageDelivered(Message message);
    }
}