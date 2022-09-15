using AutoMapper;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.Create
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IEntryRepository entryRepository;

        public CreateEntryCommandHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            this.mapper = mapper;
            this.entryRepository = entryRepository;
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var dbEntry = mapper.Map<Entry>(request);

            await entryRepository.AddAsync(dbEntry);

            return dbEntry.Id;
        }
    }
}
