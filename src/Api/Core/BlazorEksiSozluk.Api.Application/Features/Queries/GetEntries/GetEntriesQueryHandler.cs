using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if(request.TodaysEntries)
                query = query.Where(x => x.CreateDate >= DateTime.Now.Date)
                             .Where(x => x.CreateDate <= DateTime.Now.AddDays(1).Date);

            query = query.Include(x => x.EntryComments).OrderByDescending(x => x.EntryComments.Count).Take(request.Count);

            return await query.ProjectTo<GetEntriesViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
