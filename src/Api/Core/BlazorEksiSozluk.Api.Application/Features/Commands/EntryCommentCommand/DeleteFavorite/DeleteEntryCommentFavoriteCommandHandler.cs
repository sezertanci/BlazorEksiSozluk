using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.DeleteFavorite
{
    public class DeleteEntryCommentFavoriteCommandHandler : IRequestHandler<DeleteEntryCommentFavoriteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentFavoriteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new DeleteEntryCommentFavoriteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                UserId = request.UserId
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryCommentFavoriteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.DeleteEntryCommentFavoriteQueueName,
                                               obj: @obj);

            return await Task.FromResult(true);
        }
    }
}
