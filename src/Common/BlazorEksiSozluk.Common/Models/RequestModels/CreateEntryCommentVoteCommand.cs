using MediatR;

namespace BlazorEksiSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public CreateEntryCommentVoteCommand()
        {

        }

        public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid userId, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
            VoteType = voteType;
        }

        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
