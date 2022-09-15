using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Infrastructure.Extensions;
using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if(request.UserId.HasValue)
                query = query.Where(x => x.UserId == request.UserId);
            else if(!string.IsNullOrEmpty(request.UserName))
                query = query.Where(x => x.User.UserName == request.UserName);
            else return null;

            query = query.Include(x => x.EntryFavorites)
                         .Include(x => x.User);

            var list = query.Select(x => new GetUserEntriesDetailViewModel()
            {
                Content = x.Content,
                CreatedDate = x.CreateDate,
                Favorited = false,
                Subject = x.Subject,
                FavoritedCount = x.EntryFavorites.Count,
                UserId = x.UserId,
                UserName = x.User.UserName
            });

            var entries = await list.GetPaged(request.PageNumber, request.PageSize);

            return entries;
        }
    }
}
