using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.CreateFavorite
{
    public class CreateEntryCommentFavoriteCommandHandler : IRequestHandler<CreateEntryCommentFavoriteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavoriteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new CreateEntryCommentFavoriteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                UserId = request.UserId
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryCommentFavoriteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.CreateEntryCommentFavoriteQueueName,
                                               obj: @obj);

            return await Task.FromResult(true);
        }
    }
}
