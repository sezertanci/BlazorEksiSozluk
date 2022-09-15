using BlazorEksiSozluk.Common.Models.RequestModels;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        Guid GetUserId();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommand loginUserCommand);
        void Logout();
    }
}