using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Infrastructure.Extensions;
using BlazorEksiSozluk.Common.Models;
using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository entryCommentRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        this.entryCommentRepository = entryCommentRepository;
    }

    public Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = entryCommentRepository.AsQueryable();

        query = query.Include(x => x.EntryCommentFavorites)
                     .Include(x => x.EntryCommentVotes)
                     .Include(x => x.User)
                     .Where(x => x.EntryId == request.EntryId)
                     .OrderByDescending(x => x.CreateDate);

        var list = query.Select(x => new GetEntryCommentsViewModel
        {
            Id = x.Id,
            Content = x.Content,
            CreatedDate = x.CreateDate,
            Favorited = request.UserId.HasValue && x.EntryCommentFavorites.Any(y => y.UserId == request.UserId),
            FavoritedCount = x.EntryCommentFavorites.Count,
            UserId = x.User.Id,
            UserName = x.User.UserName,
            VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(y => y.UserId == request.UserId) ? x.EntryCommentVotes.FirstOrDefault(y => y.UserId == request.UserId).VoteType : VoteType.None
        });

        var entryComments = list.GetPaged(request.PageNumber, request.PageSize);

        return entryComments;
    }
}