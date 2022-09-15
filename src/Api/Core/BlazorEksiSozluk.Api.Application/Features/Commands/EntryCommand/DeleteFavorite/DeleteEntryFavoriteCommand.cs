using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteFavorite
{
    public class DeleteEntryFavoriteCommand : IRequest<bool>
    {
        public DeleteEntryFavoriteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
