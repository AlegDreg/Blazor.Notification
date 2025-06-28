using Api.Data.Entity;
using Api.Interfaces;
using Api.Interfaces.MessageRequests;
using Shared;
using Shared.Results;

namespace Api.Services
{
    /// <summary>
    /// Сервис для управления сообщениями пользователей
    /// </summary>
    /// <param name="notifyRequests"></param>
    /// <param name="messageRepository"></param>
    /// <param name="userRepository"></param>
    public class MessageService(IMessageNotifyRequests notifyRequests, IMessageRepository messageRepository, IMessageDeliveryRequests messageDelivery, IUserRepository userRepository) : IMessage
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task MessageReaded(MessageDTO messageDTO)
        {
            await messageRepository.MessageDelivered(messageDTO.Id);

            /*
            
            Могли бы сделать так ↓ , если бы у нас был личный чат. Сообщаем обоим пользователям, о прочтении
             
             */

            //  User toUser = await userRepository.GetUserByLogin(messageDTO.ToLogin) ?? throw new InvalidDataException("Адресат не найден");
            //  User fromUser = await userRepository.GetUserByLogin(messageDTO.FromLogin) ?? throw new InvalidDataException("Отправитель не найден");

            //  if (toUser.ConnectionId != null)
            //      await SendReadedNotify(messageDTO, toUser);

            //  if (fromUser.ConnectionId != null)
            //      await SendReadedNotify(messageDTO, fromUser);

            /*
            
            Оповещаем всех пользователей, т.к. у нас глобальный чат

             */
            await SendReadedNotify(messageDTO);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"/>
        public async Task<SendMessageResult> NewMessage(NewMessageDTO messageDTO)
        {
            User? toUser = await userRepository.GetUserByLogin(messageDTO.ToLogin);
            if (toUser == null)
                return new SendMessageResult { ErrorMessage = "Адресат не найден" };

            if (toUser.ConnectionId == null)
                return new SendMessageResult { ErrorMessage = "Получатель не подключен" };

            User? fromUser = await userRepository.GetUserByLogin(messageDTO.FromLogin) ?? throw new InvalidDataException("Отправитель не найден");

            var message = await messageRepository.SaveMessage(messageDTO, fromUser, toUser);
            if (message == null)
                return new SendMessageResult { ErrorMessage = "Неудалось сохранить сообщение" };

            var result = await SendMessage(message);

            await SendNotifyNewMessage(message);

            return result;
        }
        /// <summary>
        /// Отправить получателю пуш с сообщением
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<SendMessageResult> SendMessage(Message message)
        {
            ArgumentNullException.ThrowIfNull(message.To);

            await messageDelivery.SendPush(message.To.ConnectionId!, (MessageDTO)message);

            return new SendMessageResult();
        }
        /// <summary>
        /// Отправить сообщением все подключенным пользователям (кроме получателя) о новом сообщении
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Task SendNotifyNewMessage(Message message)
        {
            ArgumentNullException.ThrowIfNull(message.To);

            return notifyRequests.SendNotifyPush(message.To.ConnectionId!, (MessageDTO)message);
        }
        /// <summary>
        /// Отправить пользователю данные о том, что сообщение прочитанно
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task SendReadedNotify(MessageDTO message, User user)
        {
            await notifyRequests.SendReadedNotify(message, user);
        }
        /// <summary>
        /// Отправить всем пользователям данные о том, что сообщение прочитанно
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SendReadedNotify(MessageDTO message)
        {
            await notifyRequests.SendReadedNotify(message);
        }
    }
}