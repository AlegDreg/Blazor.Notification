using Microsoft.AspNetCore.SignalR.Client;
using Shared;
using UI.Interfaces;
using UI.Interfaces.MessageActions;

namespace UI.Services.MessageActions
{
    public class MessageStateService : IMessageTracking
    {
        private readonly IWebRequest webRequest;

        public event EventHandler<MessageDTO> GlobalMessageRecieved;
        public event EventHandler<MessageDTO> PersonalMessageReceived;
        public event EventHandler<MessageDTO> MessageReaded;
        public event EventHandler<string> NewBackgroundMessage;

        public MessageStateService(IWebRequest webRequest)
        {
            this.webRequest = webRequest;

            webRequest.HubConnection.On<MessageDTO>("NewUserMessage", (messageDTO) =>
            {
                PersonalMessageReceived?.Invoke(this, messageDTO);
            });

            webRequest.HubConnection.On<MessageDTO>("NotifyPush", (messageDTO) =>
            {
                GlobalMessageRecieved?.Invoke(this, messageDTO);
            });

            webRequest.HubConnection.On<MessageDTO>("NotifyRead", (messageDTO) =>
            {
                MessageReaded?.Invoke(this, messageDTO);
            });

            webRequest.HubConnection.On<string>("BackgroundPush", (messageDTO) =>
            {
                NewBackgroundMessage?.Invoke(this, messageDTO);
            });

            this.webRequest = webRequest;
        }
    }
}