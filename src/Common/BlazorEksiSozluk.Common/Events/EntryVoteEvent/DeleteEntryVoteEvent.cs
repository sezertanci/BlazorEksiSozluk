namespace BlazorEksiSozluk.Common.Events.EntryVoteEvent
{
    public class DeleteEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
