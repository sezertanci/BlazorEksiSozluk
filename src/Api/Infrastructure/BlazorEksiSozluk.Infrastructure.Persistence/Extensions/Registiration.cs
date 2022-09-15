using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;
using BlazorEksiSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorEksiSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registiration
    {
        public static IServiceCollection AddInfrastructureRegistiration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlazorEksiSozlukContext>(conf =>
            {
                var connString = configuration["BlazorEksiSozlukConnectionString"].ToString();

                conf.UseSqlServer(connString, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
            services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
            services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();
            services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();

            return services;
        }
    }
}
