using BlazorEksiSozluk.Common.Models;

namespace BlazorEksiSozluk.Api.Domain.Models
{
    public class EntryVote : BaseEntity
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public virtual Entry Entry { get; set; }
    }
}
