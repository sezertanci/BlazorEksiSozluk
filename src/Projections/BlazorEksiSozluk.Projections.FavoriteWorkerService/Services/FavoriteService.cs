using BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent;
using BlazorEksiSozluk.Common.Events.EntryFavoriteEvent;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorEksiSozluk.Projections.FavoriteWorkerService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFavorite(CreateEntryFavoriteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, UserId, CreateDate) VALUES(@Id, @EntryId, @UserId, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    @event.EntryId,
                    @event.UserId
                });
        }

        public async Task DeleteEntryFavorite(DeleteEntryFavoriteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE EntryId = @EntryId AND UserId = @UserId",
                new
                {
                    @event.EntryId,
                    @event.UserId
                });
        }

        public async Task CreateEntryCommentFavorite(CreateEntryCommentFavoriteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryCommentFavorite (Id, EntryCommentId, UserId, CreateDate) VALUES(@Id, @EntryCommentId, @UserId, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    @event.EntryCommentId,
                    @event.UserId
                });
        }

        public async Task DeleteEntryCommentFavorite(DeleteEntryCommentFavoriteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE EntryCommentId = @EntryCommentId AND UserId = @UserId",
                new
                {
                    @event.EntryCommentId,
                    @event.UserId
                });
        }
    }
}
