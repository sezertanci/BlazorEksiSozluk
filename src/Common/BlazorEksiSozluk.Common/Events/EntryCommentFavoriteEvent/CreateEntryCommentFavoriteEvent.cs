namespace BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent
{
    public class CreateEntryCommentFavoriteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
