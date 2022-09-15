using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQuery : IRequest<GetEntryDetailViewModel>
{
    public GetEntryDetailQuery(Guid entryId, Guid? userId)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}
