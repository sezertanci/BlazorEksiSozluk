using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentVoteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var @obj = new CreateEntryCommentVoteEvent
            {
                EntryCommentId = request.EntryCommentId,
                UserId = request.UserId,
                VoteType = request.VoteType
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryCommentVoteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.CreateEntryCommentVoteQueueName,
                                               obj: @obj);

            return await Task.FromResult(true);
        }
    }
}
