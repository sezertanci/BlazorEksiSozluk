namespace BlazorEksiSozluk.Common.Events.UserEvent
{
    public class UserEmailChangedEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
