namespace BlazorEksiSozluk.Common.Events.EntryFavoriteEvent
{
    public class CreateEntryFavoriteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
