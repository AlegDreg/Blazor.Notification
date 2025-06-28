using Shared;
using Shared.Results;
using UI.Interfaces;
using UI.Interfaces.MessageRequests;
using UI.Interfaces.Messages;

namespace UI.Services.MessageActions
{
    public class MessageDeliveryService : IMessageDelivery
    {
        private readonly IMessageSendRequests messageSend;
        private readonly IAuthRepository auth;
        /// <summary>
        /// Логин текущего пользователя
        /// </summary>
        private string? login;

        public MessageDeliveryService(IMessageSendRequests messageSend, IAuthRepository auth)
        {
            this.messageSend = messageSend;
            this.auth = auth;
        }

        /// <summary>
        /// Получить логин текущего пользователя
        /// </summary>
        /// <returns></returns>
        private async ValueTask<string> GetCurrentUserLogin()
        {
            login ??= await auth.GetUserLogin();

            ArgumentNullException.ThrowIfNull(nameof(login));

            return login!;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toLogin"></param>
        /// <returns></returns>
        public async Task<SendMessageResult?> SendMessage(string message, string toLogin)
        {
            return await messageSend.SendPush(new NewMessageDTO
            {
                ToLogin = toLogin,
                Message = message,
                FromLogin = await GetCurrentUserLogin()
            });
        }
    }
}