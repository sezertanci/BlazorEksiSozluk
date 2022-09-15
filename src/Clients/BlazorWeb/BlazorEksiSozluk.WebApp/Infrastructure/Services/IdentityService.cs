using Blazored.LocalStorage;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Infrastructure.Results;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using BlazorEksiSozluk.WebApp.Infrastructure.Auth;
using BlazorEksiSozluk.WebApp.Infrastructure.Extensions;
using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient client;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            this.syncLocalStorageService = syncLocalStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetUserName();
        }

        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand loginUserCommand)
        {
            string responseStr;
            var httpResponse = await client.PostAsJsonAsync("User/Login", loginUserCommand);

            if(httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if(httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if(!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                ((AuthStateProvider)authenticationStateProvider).NotifyUserLogin(response.UserName, response.Id);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.Token);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);

            ((AuthStateProvider)authenticationStateProvider).NotifyUserLogout();

            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
