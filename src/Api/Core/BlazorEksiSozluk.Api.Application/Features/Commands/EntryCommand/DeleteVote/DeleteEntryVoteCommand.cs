using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteVote
{
    public class DeleteEntryVoteCommand : IRequest<bool>
    {
        public DeleteEntryVoteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
