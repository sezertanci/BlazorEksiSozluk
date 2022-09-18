using BlazorEksiSozluk.Projections.UserWorkerService;
using BlazorEksiSozluk.Projections.UserWorkerService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        //services.AddScoped<UserService>();
        //services.AddScoped<EmailService>();
    })
    .Build();

await host.RunAsync();
