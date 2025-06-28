using Api.Interfaces;
using Shared.Results;

namespace Api.Services
{
    /// <summary>
    /// Сервис для работы с подключениями пользователей
    /// </summary>
    /// <param name="userRepository"></param>
    public class UserConnectionService(IUserRepository userRepository) : IUserConnection
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<AuthResult> ConnectUser(string login, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(login))
                return new AuthResult { ErrorMessage = "Логин пуст" };

            var dbUser = await userRepository.GetUserByLogin(login);

            if (dbUser == null)
            {
                dbUser = await userRepository.CreateUser(login, connectionId);

                return new AuthResult(dbUser);
            }
            else
            {
                if (dbUser.ConnectionId != null && dbUser.ConnectionId != connectionId)
                    return new AuthResult("Логин уже подключен");

                await userRepository.SetUserConnectionId(dbUser, connectionId);

                return new AuthResult(dbUser);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId"></param>
        public async Task DisconnectUser(string connectionId)
        {
            await userRepository.DisconnectUser(connectionId);
        }
    }
}