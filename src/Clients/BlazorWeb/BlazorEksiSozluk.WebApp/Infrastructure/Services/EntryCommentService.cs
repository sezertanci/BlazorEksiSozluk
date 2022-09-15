using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class EntryCommentService : IEntryCommentService
    {
        private readonly HttpClient client;

        public EntryCommentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"EntryComment/EntryComments/{entryId}?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
        {
            var response = await client.PostAsJsonAsync("EntryComment/Create", createEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
    }
}
