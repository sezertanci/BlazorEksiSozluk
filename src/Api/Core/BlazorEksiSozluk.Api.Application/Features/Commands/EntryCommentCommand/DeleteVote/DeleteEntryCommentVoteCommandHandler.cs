using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentVoteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new DeleteEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                UserId = request.UserId
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryCommentVoteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
                                               obj: @obj);

            return await Task.FromResult(true);
        }
    }
}
