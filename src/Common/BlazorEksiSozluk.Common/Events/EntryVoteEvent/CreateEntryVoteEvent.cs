using BlazorEksiSozluk.Common.Models;

namespace BlazorEksiSozluk.Common.Events.EntryVoteEvent
{
    public class CreateEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
