namespace BlazorEksiSozluk.Common.Models.Queries
{
    public class GetUserEntriesDetailViewModel : BaseFooterFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
