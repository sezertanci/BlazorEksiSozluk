using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Infrastructure.Results;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var response = await client.GetFromJsonAsync<UserDetailViewModel>($"User/{id}");

            return response;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var response = await client.GetFromJsonAsync<UserDetailViewModel>($"User/UserName/{userName}");

            return response;
        }

        public async Task<bool> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var response = await client.PostAsJsonAsync("User/Update", updateUserCommand);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);

            var response = await client.PostAsJsonAsync("User/ChangePassword", command);

            if(response != null && !response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
