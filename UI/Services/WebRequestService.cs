using Microsoft.AspNetCore.SignalR.Client;
using Shared;
using Shared.Results;
using UI.Interfaces;
using UI.Interfaces.MessageRequests;

namespace UI.Services
{
    /// <summary>
    /// Взаимодействие с апи через SignalR
    /// </summary>
    public class WebRequestService : IWebRequest, IAuthRequests, IMessageRecieveRequests, IMessageSendRequests, IMessageStateRequests
    {
        private readonly HubConnection hub;

        public HubConnection HubConnection => hub;

        public WebRequestService(HubConnection hub)
        {
            this.hub = hub;
        }

        public async Task Connect()
        {
            if (hub.State == HubConnectionState.Disconnected)
                await hub.StartAsync();
        }

        /*
         
            Можно дополнтельно вызывать автоподключение, если но не установлено, или возвращать Result<T> с текстом ошибки, или кастомное исключение пробрасывать
         
         */

        public async Task<List<MessageDTO>> GetMessages(int skip, int take)
        {
            return await hub.InvokeAsync<List<MessageDTO>>("GetMessages", new MessageRequestDTO { Skip = skip, Task = take });
        }

        public async Task<SendMessageResult?> SendPush(NewMessageDTO message)
        {
            return await hub.InvokeAsync<SendMessageResult?>("SendMessage", message);
        }

        public async Task<AuthResult?> TryAuthorize(string login)
        {
            return await hub.InvokeAsync<AuthResult?>("TryAuthorize", login);
        }

        public async Task SendReadedMessage(MessageDTO message)
        {
            await hub.SendAsync("MessageReaded", message);
        }
    }
}