using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var response = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("Entry?TodayEntries=false&Count=10");

            return response;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var response = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"Entry/{entryId}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/MainPageEntries?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetUserPageEntries(int pageNumber, int pageSize, string userName = null)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/UserEntries?userName={userName}&pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand)
        {
            var response = await client.PostAsJsonAsync("Entry/Create", createEntryCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearcBySubject(string searchText)
        {
            var response = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"Entry/Search?searchText={searchText}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"Entry/EntryComments/{entryId}?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
        {
            var response = await client.PostAsJsonAsync("Entry/CreateEntryComment", createEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
    }
}
