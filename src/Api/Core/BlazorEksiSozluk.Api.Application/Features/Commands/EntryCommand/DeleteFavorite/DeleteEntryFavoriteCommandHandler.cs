using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryFavoriteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteFavorite
{
    public class DeleteEntryFavoriteCommandHandler : IRequestHandler<DeleteEntryFavoriteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavoriteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new DeleteEntryFavoriteEvent
            {
                EntryId = request.EntryId,
                UserId = request.UserId
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryVoteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.DeleteEntryFavoriteQueueName,
                                               obj);

            return await Task.FromResult(true);
        }
    }
}
