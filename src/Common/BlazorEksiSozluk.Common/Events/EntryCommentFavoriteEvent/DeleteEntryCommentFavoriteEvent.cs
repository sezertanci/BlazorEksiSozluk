namespace BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent
{
    public class DeleteEntryCommentFavoriteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
