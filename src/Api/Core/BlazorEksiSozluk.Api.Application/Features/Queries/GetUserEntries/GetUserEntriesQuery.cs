using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }

        public GetUserEntriesQuery(Guid? userId, string userName, int pageNumber = 1, int pageSize = 10) : base(pageNumber, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
