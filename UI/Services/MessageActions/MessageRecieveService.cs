using Shared;
using UI.Interfaces.MessageActions;
using UI.Interfaces.MessageRequests;

namespace UI.Services.MessageActions
{
    /// <summary>
    /// Сервис для получения сообщений с бэка
    /// </summary>
    public class MessageRecieveService : IMessageRecieve
    {
        private readonly IMessageRecieveRequests messageRecieve;

        public MessageRecieveService(IMessageRecieveRequests messageRecieve)
        {
            this.messageRecieve = messageRecieve;
        }

        public Task<List<MessageDTO>> GetMessages(int skip, int take)
        {
            return messageRecieve.GetMessages(skip, take);
        }
    }
}