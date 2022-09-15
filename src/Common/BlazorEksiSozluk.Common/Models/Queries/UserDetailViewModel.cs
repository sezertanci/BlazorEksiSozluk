namespace BlazorEksiSozluk.Common.Models.Queries
{
    public class UserDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public bool EmailComfirmed { get; set; }
    }
}
