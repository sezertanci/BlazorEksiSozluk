using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.DeleteFavorite
{
    public class DeleteEntryCommentFavoriteCommand : IRequest<bool>
    {
        public DeleteEntryCommentFavoriteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }

        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
