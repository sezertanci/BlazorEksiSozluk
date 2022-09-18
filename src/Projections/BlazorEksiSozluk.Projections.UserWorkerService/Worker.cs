using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Infrastructure;

namespace BlazorEksiSozluk.Projections.UserWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = configuration["BlazorEksiSozlukConnectionString"];

            var userService = new Services.UserService(connectionString);
            var emailService = new Services.EmailService();

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(SozlukConstants.UserExchangeName)
                 .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
                 .Receive<UserEmailChangedEvent>(async user =>
                 {
                     var emailConfirmationId = await userService.CreateEmailConfirmation(user);

                     var emailConfirmationLink = emailService.GenerateConfirmationLink(emailConfirmationId, configuration["EmailConfirmationLink"]);

                     emailService.SendEmail(user.NewEmailAddress, emailConfirmationLink).GetAwaiter().GetResult();
                 })
                 .StartConsuming(SozlukConstants.UserEmailChangedQueueName);
        }
    }
}