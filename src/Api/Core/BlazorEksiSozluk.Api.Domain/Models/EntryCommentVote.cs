using BlazorEksiSozluk.Common.Models;

namespace BlazorEksiSozluk.Api.Domain.Models
{
    public class EntryCommentVote : BaseEntity
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public virtual EntryComment EntryComment { get; set; }
    }
}
