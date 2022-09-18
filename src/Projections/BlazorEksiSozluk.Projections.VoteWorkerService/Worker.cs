using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentVoteEvent;
using BlazorEksiSozluk.Common.Events.EntryVoteEvent;
using BlazorEksiSozluk.Common.Infrastructure;

namespace BlazorEksiSozluk.Projections.VoteWorkerService
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

            var voteService = new Services.VoteService(connectionString);

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(SozlukConstants.EntryVoteExchangeName)
                 .EnsureQueue(SozlukConstants.CreateEntryVoteQueueName, SozlukConstants.EntryVoteExchangeName)
                 .Receive<CreateEntryVoteEvent>(async vote =>
                 {
                     await voteService.CreateEntryVote(vote);
                     _logger.LogInformation($"Received EntryId : {vote.EntryId} , VoteType : {vote.VoteType}");
                 })
                 .StartConsuming(SozlukConstants.CreateEntryVoteQueueName);

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(SozlukConstants.EntryVoteExchangeName)
                 .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.EntryVoteExchangeName)
                 .Receive<DeleteEntryVoteEvent>(async vote =>
                 {
                     await voteService.DeleteEntryVote(vote);
                     _logger.LogInformation($"Received EntryId : {vote.EntryId}");
                 })
                 .StartConsuming(SozlukConstants.DeleteEntryVoteQueueName);

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(SozlukConstants.EntryCommentVoteExchangeName)
                 .EnsureQueue(SozlukConstants.CreateEntryCommentVoteQueueName, SozlukConstants.EntryCommentVoteExchangeName)
                 .Receive<CreateEntryCommentVoteEvent>(async vote =>
                 {
                     await voteService.CreateEntryCommentVote(vote);
                     _logger.LogInformation($"Received EntryCommentId : {vote.EntryCommentId} , VoteType : {vote.VoteType}");
                 })
                 .StartConsuming(SozlukConstants.CreateEntryCommentVoteQueueName);

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(SozlukConstants.EntryCommentVoteExchangeName)
                 .EnsureQueue(SozlukConstants.DeleteEntryCommentVoteQueueName, SozlukConstants.EntryCommentVoteExchangeName)
                 .Receive<DeleteEntryCommentVoteEvent>(async vote =>
                 {
                     await voteService.DeleteEntryCommentVote(vote);
                     _logger.LogInformation($"Received EntryCommentId : {vote.EntryCommentId}");
                 })
                 .StartConsuming(SozlukConstants.DeleteEntryCommentVoteQueueName);
        }
    }
}