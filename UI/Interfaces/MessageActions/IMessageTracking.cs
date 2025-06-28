using Shared;

namespace UI.Interfaces.MessageActions
{
    /// <summary>
    /// Методы и события для работы со статусами сообщений
    /// </summary>
    public interface IMessageTracking
    {
        /// <summary>
        /// В системе появилось новое сообщение любому пользователю
        /// </summary>
        event EventHandler<MessageDTO> GlobalMessageRecieved;
        /// <summary>
        /// Пришло новое сообщение
        /// </summary>
        event EventHandler<MessageDTO> PersonalMessageReceived;
        /// <summary>
        /// Сообщение прочли
        /// </summary>
        event EventHandler<MessageDTO> MessageReaded;
    }
}