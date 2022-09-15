namespace BlazorEksiSozluk.Common.Events.EntryCommentVoteEvent
{
    public class DeleteEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
