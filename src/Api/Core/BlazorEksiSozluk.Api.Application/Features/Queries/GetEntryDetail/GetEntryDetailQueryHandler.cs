using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlazorEksiSozluk.Common.Models;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository entryRepository;

    public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = entryRepository.AsQueryable();

        query = query.Include(x => x.EntryFavorites)
                     .Include(x => x.EntryVotes)
                     .Include(x => x.User)
                     .Where(x => x.Id == request.EntryId);

        var result = query.Select(x => new GetEntryDetailViewModel
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

        return await result.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}