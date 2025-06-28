using Api.Controllers;
using Api.Interfaces.MessageRequests;
using Microsoft.AspNetCore.SignalR;

namespace Api.Services
{
    public class BackgroundMessageService(IHubContext<MessageHub> hub) : IBackgroundMessages
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageToAll(string message)
        {
            return hub.Clients.All.SendAsync("BackgroundPush", message);
        }
    }
}