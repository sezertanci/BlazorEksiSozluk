namespace BlazorEksiSozluk.Projections.UserWorkerService.Services
{
    public class EmailService
    {
        public string GenerateConfirmationLink(Guid confirmationId, string configurationLink)
        {
            var baseUrl = configurationLink + confirmationId;

            return baseUrl;
        }

        public Task SendEmail(string toEmailAddress, string content)
        {
            Console.WriteLine($"Email send to {toEmailAddress} with content of {content}");

            return Task.CompletedTask;
        }
    }
}
