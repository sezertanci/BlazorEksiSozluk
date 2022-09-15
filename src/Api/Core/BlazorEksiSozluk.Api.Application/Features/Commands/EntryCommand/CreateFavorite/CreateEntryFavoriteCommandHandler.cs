using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryFavoriteEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateFavorite
{
    public class CreateEntryFavoriteCommandHandler : IRequestHandler<CreateEntryFavoriteCommand, bool>
    {
        private readonly IEntryFavoriteRepository entryFavoriteRepository;

        public CreateEntryFavoriteCommandHandler(IEntryFavoriteRepository entryFavoriteRepository)
        {
            this.entryFavoriteRepository = entryFavoriteRepository;
        }

        public async Task<bool> Handle(CreateEntryFavoriteCommand request, CancellationToken cancellationToken)
        {
            var dbEntryFavorite = new EntryFavorite
            {
                EntryId = (Guid)request.EntryId,
                UserId = (Guid)request.UserId
            };

            await entryFavoriteRepository.AddAsync(dbEntryFavorite);

            var @obj = new CreateEntryFavoriteEvent()
            {
                EntryId = request.EntryId.Value,
                UserId = request.UserId.Value
            };

            QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.EntryFavoriteExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.CreateEntryFavoriteQueueName,
                                               obj: @obj);

            return await Task.FromResult(true);
        }
    }
}
