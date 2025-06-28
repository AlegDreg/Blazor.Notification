using Blazored.LocalStorage;
using UI.Interfaces;

namespace UI.Services
{
    public class AuthRepositoryService : IAuthRepository
    {
        private string? login = null;
        private readonly ILocalStorageService storage;

        public AuthRepositoryService(ILocalStorageService storage)
        {
            this.storage = storage;
        }

        public async Task<string?> GetUserLogin()
        {
            login ??= await storage.GetItemAsStringAsync("login");
            return login;
        }

        public Task<bool> HasAuthorization() => Task.FromResult(login != null);

        public async Task<bool> SetUserLogin(string? login)
        {
            this.login = login;

            await storage.SetItemAsStringAsync("login", login ?? string.Empty);

            return true;
        }
    }
}