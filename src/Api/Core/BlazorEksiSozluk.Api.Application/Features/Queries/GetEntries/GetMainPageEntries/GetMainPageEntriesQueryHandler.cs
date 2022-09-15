using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Infrastructure.Extensions;
using BlazorEksiSozluk.Common.Models;
using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }

    public Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = entryRepository.AsQueryable();

        query = query.Include(x => x.EntryFavorites)
                     .Include(x => x.EntryVotes)
                     .Include(x => x.User)
                     .OrderByDescending(x => x.CreateDate);

        var list = query.Select(x => new GetEntryDetailViewModel
        {
            Id = x.Id,
            Content = x.Content,
            Subject = x.Subject,
            CreatedDate = x.CreateDate,
            Favorited = request.UserId.HasValue && x.EntryFavorites.Any(y => y.UserId == request.UserId),
            FavoritedCount = x.EntryFavorites.Count,
            UserId = x.User.Id,
            UserName = x.User.UserName,
            VoteType = request.UserId.HasValue && x.EntryVotes.Any(y => y.UserId == request.UserId) ? x.EntryVotes.FirstOrDefault(y => y.UserId == request.UserId).VoteType : VoteType.None
        });

        var entries = list.GetPaged(request.PageNumber, request.PageSize);

        return entries;
    }
}