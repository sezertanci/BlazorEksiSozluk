namespace BlazorEksiSozluk.WebApp.Infrastructure.Models
{
    public class VoteClickedEventArgs
    {
        public Guid? EntryId { get; set; }
        public Guid? EntryCommentId { get; set; }

        public bool IsUpVoteClicked { get; set; }
        public bool IsUpVoteDeleted { get; set; }

        public bool IsDownVoteClicked { get; set; }
        public bool IsDownVoteDeleted { get; set; }
    }
}
