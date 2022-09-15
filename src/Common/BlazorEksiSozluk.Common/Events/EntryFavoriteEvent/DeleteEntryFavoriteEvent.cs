namespace BlazorEksiSozluk.Common.Events.EntryFavoriteEvent
{
    public class DeleteEntryFavoriteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
