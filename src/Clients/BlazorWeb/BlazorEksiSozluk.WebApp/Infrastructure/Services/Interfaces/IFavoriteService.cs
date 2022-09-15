namespace BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task CreateEntryCommentFavorite(Guid entryCommentId);
        Task CreateEntryFavorite(Guid entryId);
        Task DeleteEntryCommentFavorite(Guid entryCommentId);
        Task DeleteEntryFavorite(Guid entryId);
    }
}
