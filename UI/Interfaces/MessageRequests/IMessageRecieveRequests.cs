using Shared;

namespace UI.Interfaces.MessageRequests
{
    /// <summary>
    /// Методы для получения сообщений
    /// </summary>
    public interface IMessageRecieveRequests
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