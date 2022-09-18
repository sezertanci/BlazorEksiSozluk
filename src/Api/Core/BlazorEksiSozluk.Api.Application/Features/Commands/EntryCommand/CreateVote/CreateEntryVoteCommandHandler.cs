using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryVoteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        //private readonly IEntryVoteRepository entryVoteRepository;

        //public CreateEntryVoteCommandHandler(IEntryVoteRepository entryVoteRepository)
        //{
        //    this.entryVoteRepository = entryVoteRepository;
        //}

        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            //Direk Veri tabanına yazar

            //var dbEntryVotete = new EntryVote
            //{
            //    EntryId = request.EntryId,
            //    UserId = request.UserId,
            //    VoteType = request.VoteType
            //};

            //await entryVoteRepository.AddAsync(dbEntryVotete);

            var @obj = new CreateEntryVoteEvent
            {
                EntryId = request.EntryId,
                UserId = request.UserId,
                VoteType = request.VoteType
            };

            //RabbitMQ aracılığıyla veri tabanına yazar
            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryVoteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.CreateEntryVoteQueueName,
                                               obj);

            return await Task.FromResult(true);
        }
    }
}
