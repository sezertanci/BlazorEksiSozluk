using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntries.GetMainPageEntries;

public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
{
    public GetMainPageEntriesQuery(Guid? userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
        UserId = userId;
    }

    public Guid? UserId { get; set; }
}
