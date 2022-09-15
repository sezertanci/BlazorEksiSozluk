using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryVoteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new DeleteEntryVoteEvent
            {
                EntryId = request.EntryId,
                UserId = request.UserId
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryVoteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.DeleteEntryVoteQueueName,
                                               obj);

            return await Task.FromResult(true);
        }
    }
}
