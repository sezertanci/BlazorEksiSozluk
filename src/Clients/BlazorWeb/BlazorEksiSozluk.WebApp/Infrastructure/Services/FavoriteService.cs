using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly HttpClient client;

        public FavoriteService(HttpClient client)
        {
            this.client = client;
        }

        public async Task CreateEntryCommentFavorite(Guid entryCommentId)
        {
            var response = await client.PostAsync($"Favorite/EntryComment/{entryCommentId}", null);
        }

        public async Task CreateEntryFavorite(Guid entryId)
        {
            var response = await client.PostAsync($"Favorite/Entry/{entryId}", null);
        }

        public async Task DeleteEntryCommentFavorite(Guid entryCommentId)
        {
            var response = await client.PostAsync($"Favorite/DeleteEntryCommentFavorite/{entryCommentId}", null);
        }

        public async Task DeleteEntryFavorite(Guid entryId)
        {
            var response = await client.PostAsync($"Favorite/DeleteEntryFavorite/{entryId}", null);
        }
    }
}
