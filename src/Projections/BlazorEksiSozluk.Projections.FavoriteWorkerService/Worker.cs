using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.EntryCommentFavoriteEvent;
using BlazorEksiSozluk.Common.Events.EntryFavoriteEvent;
using BlazorEksiSozluk.Common.Infrastructure;

namespace BlazorEksiSozluk.Projections.FavoriteWorkerService;

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

        var favoriteService = new Services.FavoriteService(connectionString);

        QueryFactory.CreateBasicConsumer()
             .EnsureExchange(SozlukConstants.EntryFavoriteExchangeName)
             .EnsureQueue(SozlukConstants.CreateEntryFavoriteQueueName, SozlukConstants.EntryFavoriteExchangeName)
             .Receive<CreateEntryFavoriteEvent>(async fav =>
             {
                 await favoriteService.CreateEntryFavorite(fav);
                 _logger.LogInformation($"Received EntryId {fav.EntryId}");
             })
             .StartConsuming(SozlukConstants.CreateEntryFavoriteQueueName);

        QueryFactory.CreateBasicConsumer()
             .EnsureExchange(SozlukConstants.EntryFavoriteExchangeName)
             .EnsureQueue(SozlukConstants.DeleteEntryFavoriteQueueName, SozlukConstants.EntryFavoriteExchangeName)
             .Receive<DeleteEntryFavoriteEvent>(async fav =>
             {
                 await favoriteService.DeleteEntryFavorite(fav);
                 _logger.LogInformation($"Received EntryId {fav.EntryId}");
             })
             .StartConsuming(SozlukConstants.DeleteEntryFavoriteQueueName);

        QueryFactory.CreateBasicConsumer()
             .EnsureExchange(SozlukConstants.EntryCommentFavoriteExchangeName)
             .EnsureQueue(SozlukConstants.CreateEntryCommentFavoriteQueueName, SozlukConstants.EntryCommentFavoriteExchangeName)
             .Receive<CreateEntryCommentFavoriteEvent>(async fav =>
             {
                 await favoriteService.CreateEntryCommentFavorite(fav);
                 _logger.LogInformation($"Received EntryCommentId {fav.EntryCommentId}");
             })
             .StartConsuming(SozlukConstants.CreateEntryCommentFavoriteQueueName);

        QueryFactory.CreateBasicConsumer()
             .EnsureExchange(SozlukConstants.EntryCommentFavoriteExchangeName)
             .EnsureQueue(SozlukConstants.DeleteEntryCommentFavoriteQueueName, SozlukConstants.EntryCommentFavoriteExchangeName)
             .Receive<DeleteEntryCommentFavoriteEvent>(async fav =>
             {
                 await favoriteService.DeleteEntryCommentFavorite(fav);
                 _logger.LogInformation($"Received EntryCommentId {fav.EntryCommentId}");
             })
             .StartConsuming(SozlukConstants.DeleteEntryCommentFavoriteQueueName);
    }
}