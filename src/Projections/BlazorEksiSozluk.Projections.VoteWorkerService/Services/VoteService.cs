using BlazorEksiSozluk.Common.Events.EntryCommentVoteEvent;
using BlazorEksiSozluk.Common.Events.EntryVoteEvent;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEksiSozluk.Projections.VoteWorkerService.Services
{
    public class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote(CreateEntryVoteEvent @event)
        {
            var deleteEntryVoteEvent = new DeleteEntryVoteEvent
            {
                EntryId = @event.EntryId,
                UserId = @event.UserId,
            };

            await DeleteEntryVote(deleteEntryVoteEvent);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryVote (Id, EntryId, UserId, VoteType, CreateDate) VALUES(@Id, @EntryId, @UserId, @VoteType, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    @event.EntryId,
                    @event.UserId,
                    voteType = (int)@event.VoteType
                });
        }

        public async Task DeleteEntryVote(DeleteEntryVoteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId = @EntryId AND UserId = @UserId",
                new
                {
                    @event.EntryId,
                    @event.UserId
                });
        }

        public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent @event)
        {
            var deleteEntryCommentVoteEvent = new DeleteEntryCommentVoteEvent
            {
                EntryCommentId = @event.EntryCommentId,
                UserId = @event.UserId,
            };

            await DeleteEntryCommentVote(deleteEntryCommentVoteEvent);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryCommentVote (Id, EntryCommentId, UserId, VoteType, CreateDate) VALUES(@Id, @EntryCommentId, @UserId, @VoteType, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    @event.EntryCommentId,
                    @event.UserId,
                    voteType = (int)@event.VoteType
                });
        }

        public async Task DeleteEntryCommentVote(DeleteEntryCommentVoteEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryCommentVote WHERE EntryCommentId = @EntryCommentId AND UserId = @UserId",
                new
                {
                    @event.EntryCommentId,
                    @event.UserId
                });
        }
    }
}
