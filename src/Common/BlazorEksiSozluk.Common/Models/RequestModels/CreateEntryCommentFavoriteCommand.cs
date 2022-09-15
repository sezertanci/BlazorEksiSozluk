using MediatR;

namespace BlazorEksiSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentFavoriteCommand : IRequest<bool>
    {
        public CreateEntryCommentFavoriteCommand()
        {

        }

        public CreateEntryCommentFavoriteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }

        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
