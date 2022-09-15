using BlazorEksiSozluk.Common.Models;
using BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient client;

        public VoteService(HttpClient client)
        {
            this.client = client;
        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }

        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var response = await client.PostAsync($"Vote/EntryVote/{entryId}?voteType={voteType}", null);

            if(!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage.ToString());

            return response;
        }

        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var response = await client.PostAsync($"Vote/EntryCommentVote/{entryCommentId}?voteType={voteType}", null);

            if(!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage.ToString());

            return response;
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await client.PostAsync($"Vote/DeleteEntryVote/{entryId}", null);

            if(!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage.ToString());
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await client.PostAsync($"Vote/DeleteEntryCommentVote/{entryCommentId}", null);

            if(!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage.ToString());
        }
    }
}
