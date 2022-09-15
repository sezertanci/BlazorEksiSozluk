using Blazored.LocalStorage;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string UserName = "username";
        public const string UserId = "userid";

        public static bool IsUserLoggedIn(this ISyncLocalStorageService syncLocalStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(syncLocalStorageService));
        }

        public static string GetUserName(this ISyncLocalStorageService syncLocalStorageService)
        {
            return syncLocalStorageService.GetItem<string>(UserName);
        }

        public static async Task<string> GetUserName(this ILocalStorageService syncLocalStorageService)
        {
            return await syncLocalStorageService.GetItemAsync<string>(UserName);
        }

        public static void SetUserName(this ISyncLocalStorageService syncLocalStorageService, string value)
        {
            syncLocalStorageService.SetItem(UserName, value);
        }

        public static async Task SetUserName(this ILocalStorageService syncLocalStorageService, string value)
        {
            await syncLocalStorageService.SetItemAsync(UserName, value);
        }

        public static Guid GetUserId(this ISyncLocalStorageService syncLocalStorageService)
        {
            return syncLocalStorageService.GetItem<Guid>(UserId);
        }

        public static void SetUserId(this ISyncLocalStorageService syncLocalStorageService, Guid id)
        {
            syncLocalStorageService.SetItem(UserId, id);
        }

        public static async Task SetUserId(this ILocalStorageService syncLocalStorageService, Guid id)
        {
            await syncLocalStorageService.SetItemAsync(UserId, id);
        }

        public static async Task<Guid> GetUserId(this ILocalStorageService syncLocalStorageService)
        {
            return await syncLocalStorageService.GetItemAsync<Guid>(UserId);
        }

        public static string GetToken(this ISyncLocalStorageService syncLocalStorageService)
        {
            var token = syncLocalStorageService.GetItem<string>(TokenName);

            return token;
        }

        public static async Task<string> GetToken(this ILocalStorageService syncLocalStorageService)
        {
            var token = await syncLocalStorageService.GetItemAsync<string>(TokenName);

            return token;
        }

        public static void SetToken(this ISyncLocalStorageService syncLocalStorageService, string value)
        {
            syncLocalStorageService.SetItem(TokenName, value);
        }

        public static async Task SetToken(this ILocalStorageService syncLocalStorageService, string value)
        {
            await syncLocalStorageService.SetItemAsync(TokenName, value);
        }
    }
}
