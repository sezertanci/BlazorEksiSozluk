using BlazorEksiSozluk.Common.Models.Page;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;

namespace BlazorEksiSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetUserPageEntries(int pageNumber, int pageSize, string userName = null);
        Task<List<SearchEntryViewModel>> SearcBySubject(string searchText);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize);
    }
}