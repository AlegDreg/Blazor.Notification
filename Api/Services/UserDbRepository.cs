using Api.Data.Entity;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    /// <summary>
    /// Сервис для работы с бд пользователей
    /// </summary>
    /// <param name="db"></param>
    public class UserDbRepository(AppDbContext db) : IUserRepository
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllConnections()
        {
            await db.Users
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(u => u.ConnectionId, u => null));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(string login, string connectionId)
        {
            var user = db.Users.Add(new User
            {
                Login = login,
                ConnectionId = connectionId,
                DateConnect = DateTime.Now,
            }).Entity;
            await db.SaveChangesAsync();

            return user;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="connectionId"></param>
        public async Task DisconnectUser(string connectionId)
        {
            await db.Users
                .Where(u => u.ConnectionId == connectionId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(u => u.ConnectionId, u => null));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByLogin(string login)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SetUserConnectionId(User dbUser, string connectionId)
        {
            dbUser.ConnectionId = connectionId;

            await db.SaveChangesAsync();
        }
    }
}