using Shared;

namespace UI.Interfaces.MessageActions
{
    /// <summary>
    /// Методы для получения сообщений
    /// </summary>
    public interface IMessageRecieve
    {
        /// <summary>
        /// Получить сообщения с бэка
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<MessageDTO>> GetMessages(int skip, int take);
    }
}