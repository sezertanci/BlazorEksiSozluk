using BlazorEksiSozluk.Common.Models;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateVote
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {
        public CreateEntryVoteCommand(Guid entryId, Guid userId, VoteType voteType)
        {
            EntryId = entryId;
            UserId = userId;
            VoteType = voteType;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
