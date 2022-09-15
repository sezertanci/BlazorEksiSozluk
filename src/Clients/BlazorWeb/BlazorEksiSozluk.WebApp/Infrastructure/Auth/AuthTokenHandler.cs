using Blazored.LocalStorage;
using BlazorEksiSozluk.WebApp.Infrastructure.Extensions;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Auth
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            this.syncLocalStorageService = syncLocalStorageService;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = syncLocalStorageService.GetToken();

            if(!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
