namespace BlazorEksiSozluk.Common.Models.Queries
{
    public class GetEntryCommentsViewModel : BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
