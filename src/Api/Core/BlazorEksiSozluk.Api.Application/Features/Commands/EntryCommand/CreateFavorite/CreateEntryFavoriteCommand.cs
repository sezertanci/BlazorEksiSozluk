using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateFavorite
{
    public class CreateEntryFavoriteCommand : IRequest<bool>
    {
        public CreateEntryFavoriteCommand(Guid? entryId, Guid? userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
