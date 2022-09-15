using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}
