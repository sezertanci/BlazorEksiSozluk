using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryCommentService
    {
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize);
    }
}